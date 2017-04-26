using UnityEngine;
using System.Collections;
using System;

public class PlayerFlightControl : MonoBehaviour {

    // Use this for initialization
    [SerializeField]
    float m_Speed = 20f;
    float m_MinSpeed;
    float m_MaxSpeed;
    [SerializeField]
    protected float shield = 5;
    public float MaxShield { get; private set; }
    public float Shield { get { return shield; } }
    [SerializeField]
    float brakeVariable = 2f;
    [SerializeField]
    float m_TurnSpeed = 180f;
    [SerializeField]
    AudioSource m_MovementAudio;
    [SerializeField]
     AudioClip m_EngineIdling;
    [SerializeField]
    AudioClip m_EngineDriving;
    [SerializeField]
    Camera cameraAlignment;

    [SerializeField]
    Transform cameraAlignmentTool;
    [SerializeField]
    GameObject Water;

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
    bool isGoing = false;
    bool belowWater;
    //AutoRotation variables 
    public float autoSpeed = 10F;
    private float startTime;
    private float journeyLength;
    private float recoverTime = 5f;
  
    void Start()
    {
        GetComponent<Rigidbody>().rotation = Quaternion.identity;
       
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
        MaxShield = shield;
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
         m_HorizontalInputValue = Input.GetAxis("Horizontal" + m_PlayerNumber);
         m_VerticalInputValue = Input.GetAxis("Vertical" + m_PlayerNumber);
         isGoing = Input.GetButton("Jump" + m_PlayerNumber);   
         float anyInput = m_VerticalInputValue + m_HorizontalInputValue;
        if (isGoing == true) //Input.GetButtonDown("JUmp")
        {         
            m_Speed += autoSpeed * Time.deltaTime;
        }
        else if (isGoing == false)
        {
            m_Speed -= autoSpeed / brakeVariable * Time.deltaTime;
        }
    }
    void FixedUpdate()
    {
        Quaternion deltaRotation = Quaternion.Euler(eulerAngleVelocity * Time.deltaTime);
        m_Rigidbody = GetComponent<Rigidbody>();

        AutoEnvironmentCheck();
        Roll();  //Roll is tilting left/right (X axis)
        Pitch(); //Pitch is tilt Up/Down (Y axis)
        Yaw();  //Yaw is moving forward/back (Z axis    
        AutoRotate();
        AutoMovement();
        AutoShieldRecover();     
    }

    public virtual void AutoShieldRecover()
    {
        if (recoverTime == 0)
        {
            shield += MaxShield * Time.deltaTime;
        }
        if(shield == 0)
        {

        }      
    }


    private void AutoEnvironmentCheck()
    {        
        if (transform.position.y <= Water.transform.position.y)
        {
            belowWater = true;
        }
        else belowWater = false;             
    }

    private void AutoMovement()
    {
        m_MinSpeed = 10f;
        m_MaxSpeed = 80f;
        transform.Translate(Vector3.forward * m_Speed * Time.deltaTime);
        m_Speed -= transform.forward.y * 2.0f * Time.deltaTime * 10f;
        if(m_Speed < m_MinSpeed)
        {
            m_Speed = m_MinSpeed;
        }
        else if (m_Speed > m_MaxSpeed)
        {
            m_Speed = m_MaxSpeed;
        }
        else if (belowWater == true)
        {
            m_MaxSpeed = 3.5f;
            autoSpeed = 10f;
            m_TurnSpeed = 30f;
            if (m_Speed < 1.5f && belowWater)
            {
                m_Speed = 1.5f;
               
            }
        }    
    }
    private void AutoRotate()
    {

        currentPosition = transform.position;      
        newPosition = cameraAlignmentTool.transform.position;
        Quaternion currentRotation = m_Rigidbody.rotation;
        Quaternion balancedRotation = cameraAlignment.transform.rotation;     
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
