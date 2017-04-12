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

    //Variables private
    Vector3 eulerAngleVelocity;

    Rigidbody m_Rigidbody;
    string m_MovementAxisName;
    string m_TurnAxisName;


    //Pitch, Roll, Yaw
    //Pitch is tilt Up/Down (Y axis)  || Vertical refactor to Pitch
    //Yaw is moving forward/back (Z axis  || Movement refactor to Yaw
    //Roll is tilting left/right (X axis) || Turn refactor to Roll
    float m_HorizontalInputValue;
    float m_VerticalInputValue;
    float m_OriginalPitch;
    
    //mvem
    /// <summary>
    //
    /// </summary>
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
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update () {
        m_HorizontalInputValue = Input.GetAxis("Horizontal");
        m_VerticalInputValue = Input.GetAxis("Vertical");
    }
    void FixedUpdate()
    {
        Quaternion deltaRotation = Quaternion.Euler(eulerAngleVelocity * Time.deltaTime);
        m_Rigidbody = GetComponent<Rigidbody>();
        //rb is now m_Rigidbody
       
        Roll();  //Roll is tilting left/right (X axis)
        Pitch(); //Pitch is tilt Up/Down (Y axis)
        Yaw();  //Yaw is moving forward/back (Z axis
        AutoRotate();
        AutoMovement();
    }

    private void AutoMovement()
    {
        transform.Translate(Vector3.forward * m_Speed * Time.deltaTime);
    }

    private void AutoRotate()
    {
        Quaternion balancedRotation = Quaternion.Euler(0f, 0f, 0f);
        //  if(currentRotation != balancedRotation)
        //{ if (playerMovementInput == 0)}
    }
    private void Roll()
    {
        // Adjust the rotation of the tank based on the player's input.
        //moveHorizonal is now roll 
        float roll = m_VerticalInputValue * m_TurnSpeed * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(roll, 0f, 0f);
        m_Rigidbody.MoveRotation(m_Rigidbody.rotation * turnRotation);
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
        float yaw = m_HorizontalInputValue * m_TurnSpeed * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, 0f, yaw);
        m_Rigidbody.MoveRotation(m_Rigidbody.rotation * turnRotation);
        // Vector3 movement = transform.forward * m_VerticalInputValue * m_Speed * Time.deltaTime;
    }
}
