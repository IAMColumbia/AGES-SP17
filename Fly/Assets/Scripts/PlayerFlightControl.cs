using UnityEngine;
using System.Collections;
using System;

public class PlayerFlightControl : MonoBehaviour {

    // Use this for initialization
    [SerializeField]
    float m_Speed = 12f;
    [SerializeField]
    float m_TurnSpeed = 180f;
    [SerializeField]
    AudioSource m_MovementAudio;
    [SerializeField]
     AudioClip m_EngineIdling;
    [SerializeField]
    AudioClip m_EngineDriving;
    [SerializeField]
    Camera camera;

    //Variables private
    Vector3 eulerAngleVelocity;
    Vector3 currentPosition;
    Vector3 newPosition;

    Rigidbody m_Rigidbody;
    const float Z_ANGLE_MIN = -15F;
    const float Z_ANGLE_MAX = 15F;
    string m_MovementAxisName;
    string m_TurnAxisName;

    float m_HorizontalInputValue;
    float m_VerticalInputValue;
    //AutoRotation variables 
    public float autoSpeed = 60F;
    private float startTime;
    private float journeyLength;
  
    void Start()
    {
       
    }

    //Pitch, Roll, Yaw
    //Pitch is tilt Up/Down (Y axis)  || Vertical refactor to Pitch
    //Yaw is moving forward/back (Z axis  || Movement refactor to Yaw
    //Roll is tilting left/right (X axis) || Turn refactor to Roll

    float m_OriginalPitch;

    //Public variables go here. 
    public int m_PlayerNumber = 1;
  
    void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }
    void OnEnable()
    {
        m_Rigidbody.isKinematic = false;
        //m_movementInput
        m_HorizontalInputValue = 0f;
        //m_turnInput
        m_VerticalInputValue = 0f;
    }
    private void OnDisable()
    {
        m_Rigidbody.isKinematic = true;
    }
    // Update is called once per frame
    void Update () {
        //m_HorizontalInputValue = Input.GetAxis("Horizontal");
        //m_VerticalInputValue = Input.GetAxis("Vertical");
         m_HorizontalInputValue = Input.GetAxis("Horizontal" + m_PlayerNumber);
         m_VerticalInputValue = Input.GetAxis("Vertical" + m_PlayerNumber);
       
    }
    void FixedUpdate()
    {
        Quaternion deltaRotation = Quaternion.Euler(eulerAngleVelocity * Time.deltaTime);
        m_Rigidbody = GetComponent<Rigidbody>();
             
        Roll();  //Roll is tilting left/right (X axis)
        Pitch(); //Pitch is tilt Up/Down (Y axis)
        Yaw();  //Yaw is moving forward/back (Z axis
        AutoRotate();
        AutoMovement();
    }

    private void AutoMovement()
    {     
        transform.Translate(Vector3.forward * m_Speed * Time.deltaTime);
        m_Speed -= transform.forward.y * 2.0f * Time.deltaTime * 10f;

        if(m_Speed < 20.0f)
        {
            m_Speed = 20.0f;
        }
       // m_Speed += transform.forward.y * 2.0f * Time.deltaTime;
    }
    private void AutoRotate()
    {
        currentPosition = transform.position;
       
        Quaternion balancedRotation = Quaternion.Euler(camera.transform.rotation.x, camera.transform.rotation.y, camera.transform.rotation.z);
        Quaternion currentRotation = m_Rigidbody.rotation;
        journeyLength = Vector3.Distance(currentPosition, newPosition);
        float distCovered = (Time.time - startTime) * autoSpeed;
        float fracJourney = distCovered / journeyLength;
        startTime = Time.time;
        Quaternion autoRotation = Quaternion.Lerp(currentRotation, balancedRotation, fracJourney);
        if (currentRotation != balancedRotation)
        {
            //Calculates whether a large enough input was made. 
            float anyInput = m_VerticalInputValue + m_HorizontalInputValue;
            if (anyInput < .25f)
            {             
                balancedRotation.y = camera.transform.rotation.y;            
                m_Rigidbody.MoveRotation(autoRotation);                
            }
            if (anyInput < .25f)
            {                
                balancedRotation.x = camera.transform.rotation.x;             
                m_Rigidbody.MoveRotation(autoRotation);
            }
            if(anyInput < .25f)
            {              
                balancedRotation.z = camera.transform.rotation.z;
                m_Rigidbody.MoveRotation(autoRotation);                    
            }                   
        }
    }
    private void Roll()
    {
        // Adjust the rotation of the tank based on the player's input.
        //moveHorizonal is now roll 
        float roll = m_VerticalInputValue * m_TurnSpeed * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(-roll, 0f, 0f);
        m_Rigidbody.MoveRotation(m_Rigidbody.rotation * turnRotation);
        m_Rigidbody.MovePosition(Vector3.up);
    }
    private void Pitch()
    {
        //moveHorizonal is now pitch       
        float pitch = m_HorizontalInputValue * m_TurnSpeed * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(0, pitch, 0f);
        m_Rigidbody.MoveRotation(m_Rigidbody.rotation * turnRotation);
    }
    private void Yaw()
    {
        //moveHorizonal is now yaw
        //float currentZ;
        float yaw = m_HorizontalInputValue * m_TurnSpeed * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, 0f, -yaw);
        m_Rigidbody.MoveRotation(m_Rigidbody.rotation * turnRotation);
        m_Rigidbody.MovePosition(Vector3.left);
        yaw = Mathf.Clamp(transform.rotation.z, Z_ANGLE_MIN, Z_ANGLE_MAX);     
        // Vector3 movement = transform.forward * m_VerticalInputValue * m_Speed * Time.deltaTime;
    }
}
