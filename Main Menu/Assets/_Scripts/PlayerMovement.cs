using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int playerNumber = 1;
    public float speed = 12f;
    public float turnSpeed = 180f;

    //public AudioSource m_MovementAudio;
    //public AudioClip m_EngineIdling;
    //public AudioClip m_EngineDriving;
    //public float m_PitchRange = 0.2f;

    string moveHorizontal;
    string moveVertical;
    Rigidbody rigidBody;
    float horizontalInputValue;
    float verticalInputValue;
    float originalPitch;


    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }


    void OnEnable()
    {
        rigidBody.isKinematic = false;
        horizontalInputValue = 0f;
        verticalInputValue = 0f;
    }


    void OnDisable()
    {
        rigidBody.isKinematic = true;
    }


    void Start()
    {

        moveVertical = "Vertical" + playerNumber;
        moveHorizontal = "Horizontal" + playerNumber;

        //m_OriginalPitch = m_MovementAudio.pitch;
    }

    void Update()
    {
        // Store the player's input and make sure the audio for the engine is playing.
        horizontalInputValue = Input.GetAxis(moveHorizontal);
        verticalInputValue = Input.GetAxis(moveVertical);

        //EngineAudio();
    }


    //private void EngineAudio()
    //{
    //    // Play the correct audio clip based on whether or not the tank is moving and what audio is currently playing.
    //    if (Mathf.Abs(m_MovementInputValue) < 0.1f && Mathf.Abs(m_TurnInputValue) < 0.1f)
    //    {
    //        if (m_MovementAudio.clip == m_EngineDriving)
    //        {
    //            m_MovementAudio.clip = m_EngineIdling;
    //            m_MovementAudio.pitch = Random.Range(m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
    //            m_MovementAudio.Play();
    //        }
    //    }
    //    else
    //    {
    //        if (m_MovementAudio.clip == m_EngineIdling)
    //        {
    //            m_MovementAudio.clip = m_EngineDriving;
    //            m_MovementAudio.pitch = Random.Range(m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
    //            m_MovementAudio.Play();
    //        }
    //    }

    //}


    void FixedUpdate()
    {
        // Move and turn the tank.
        Move();
    }

    void Move()
    {
        // Adjust the position of the tank based on the player's input.
        Vector3 movement = new Vector3(horizontalInputValue, 0.0f, verticalInputValue);

        rigidBody.AddForce(movement * speed);
    }


    //void Turn()
    //{
    //    // Adjust the rotation of the tank based on the player's input.
    //    float turn = verticalInputValue * turnSpeed * Time.deltaTime;

    //    Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);

    //    rigidBody.MoveRotation(rigidBody.rotation * turnRotation);
    //}
}
