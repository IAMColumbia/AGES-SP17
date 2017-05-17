using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class PlayerFlightControl : GameManager {

    // Use this for initialization
    GameManager gameManager;
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
    AudioSource collectSource;
    [SerializeField]
    AudioClip collectAudio;
    private float m_OriginalPitch;
    public float m_PitchRange = 0.2f;

    Camera camera;

    [SerializeField]
    GameObject respawn;

    [SerializeField]
    Transform cameraAlignmentTool;
   
    [SerializeField]
    Text countText;
    float checkPointTextTime = 2.5f;

    GameObject water;

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
    bool respawnPlayer = false;
    bool belowWater;
    bool isTriggered;
    //AutoRotation variables 
    public float autoRotationSpeed = 10F;
    private float startTime;
    private float journeyLength;
    private float recoverTime = 5f;

    void Start()
    {
        GameManager gameManager = GetComponent<GameManager>();
        GetComponent<Rigidbody>().rotation = Quaternion.identity;
        water = GameObject.FindGameObjectWithTag("Water");
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        countText = GameObject.Find("Rings Collected").GetComponent<Text>();
        m_OriginalPitch = m_MovementAudio.pitch;     
    }

    //Public variables go here.
    public int ringCount = 0; 
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
        respawnPlayer = Input.GetButtonDown("Respawn" + m_PlayerNumber);
         float anyInput = m_VerticalInputValue + m_HorizontalInputValue;
        if (isGoing == true) //Input.GetButtonDown("JUmp")
        {
            m_Speed += autoRotationSpeed * Time.deltaTime;
            TextBoxCheck();
        }
        else if (isGoing == false)
        {
            m_Speed -= brakeVariable / autoRotationSpeed * Time.deltaTime;
          
                textBox.SetActive(true);
                m_MessageText.text = "Would you like to respawn? (Press R)";
                if(respawnPlayer == true)
                {
                    Respawn();
                }         
            EngineAudio();
        }
    }
    void FixedUpdate()
    {
        Quaternion deltaRotation = Quaternion.Euler(eulerAngleVelocity * Time.deltaTime);
        m_Rigidbody = GetComponent<Rigidbody>();

        //AutoEnvironmentCheck();
        GameCheck();
        Roll();  //Roll is tilting left/right (X axis)
        Pitch(); //Pitch is tilt Up/Down (Y axis)
        Yaw();  //Yaw is moving forward/back (Z axis    
        AutoRotate();
        AutoMovement();
      
        AutoShieldRecover();     
    }

    private void TextBoxCheck()
    {
       
        if (textBox.activeSelf)
        {
            checkPointTextTime = .5f;
            checkPointTextTime -= Time.deltaTime;
            if (checkPointTextTime <= 0)
            {
                m_MessageText.text = "";
                textBox.SetActive(false);          
            }
        }
    }

    private void EngineAudio()
    {
     
        if (isGoing == true)
        {
            if (m_MovementAudio.clip == m_EngineDriving)
            {
                m_MovementAudio.clip = m_EngineIdling;
                m_MovementAudio.pitch = UnityEngine.Random.Range(m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
                m_MovementAudio.Play();
               
            }
        }
        else
        {
            //This transformrotation will MAYBE fix any collision errors with flight control.
            transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w);
            if (m_MovementAudio.clip == m_EngineIdling)
            {
                m_MovementAudio.clip = m_EngineDriving;
                m_MovementAudio.pitch = UnityEngine.Random.Range(m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
                m_MovementAudio.Play();
            }
        }
    }
    private void GameCheck()
    {
      
       
        if (ringCount >= 25)
        {
            roundOneDone = true;
            round1.SetActive(false);
            Debug.Log("Player Flight Control: Round 1 Done");
                                    
        }
        if(ringCount >= 50 && roundOneDone == true)
        {
            roundTwoDone = true;
            round2.SetActive(false);
            Debug.Log("Player Flight Control: Round 2 Done");
           
        }
        if (ringCount >= 75 && roundTwoDone == true)
        {
            roundThreeDone = true;
            round3.SetActive(false);
            Debug.Log("Player Flight Control: Round 3 Done");
           
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ring Trigger")
        {
            collectSource.clip = collectAudio;
            collectSource.Play();
                other.gameObject.SetActive(false);                     
            textBox.SetActive(true);
            ringCount++;
            m_MessageText.text = "Ring " + ringCount;
            Debug.Log("Ring Count:" + ringCount);
            SetCountText();
            
        }
        if(other.gameObject.tag == "Hazard")
        {
            this.gameObject.SetActive(false);
            Respawn();
        }
    }

    private void Respawn()
    {
        transform.position = respawn.transform.position;
        transform.rotation = respawn.transform.rotation;
        this.gameObject.SetActive(true);
    }

    void OnTriggerExit(Collider other)
    {     
        Destroy(other);
        TextBoxCheck();
    }

    private void SetCountText()
    {
        countText.text = ringCount.ToString();
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
            autoRotationSpeed = 10f;
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
        Quaternion balancedRotation = camera.transform.rotation;     
        journeyLength = Vector3.Distance(currentPosition, newPosition);
        float distCovered = (Time.time - startTime) * autoRotationSpeed;
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
        m_Speed -= Time.deltaTime;
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
        m_Speed -= Time.deltaTime;
        // Vector3 movement = transform.forward * m_VerticalInputValue * m_Speed * Time.deltaTime;
    }
}
