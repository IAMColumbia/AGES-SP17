using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    public int m_PlayerNumber = 1;
    public float m_Force = 20f;
    public float m_TurnSpeed = 180f;
    //public AudioSource m_MovementAudio;
    //public AudioClip m_EngineIdling;
    //public AudioClip m_EngineDriving;
    //public float m_PitchRange = 0.2f;

    private string m_MovementAxisName;
    private string m_TurnAxisName;
    private Rigidbody m_Rigidbody;
    private float m_MovementInputValue;
    private float m_TurnInputValue;
    private const float k_GroundRayLength = 1f; // The length of the ray to check if the ball is grounded.
    //private float m_OriginalPitch;

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }


    // Use this for initialization
    void Start () {
        m_MovementAxisName = "Vertical" + m_PlayerNumber;
        m_TurnAxisName = "Horizontal" + m_PlayerNumber;

        //m_OriginalPitch = m_MovementAudio.pitch;
    }

    // Update is called once per frame
    void Update () {
        // Store the player's input and make sure the audio for the engine is playing.
        m_MovementInputValue = Input.GetAxis(m_MovementAxisName);
        m_TurnInputValue = Input.GetAxis(m_TurnAxisName);

        //EngineAudio();
    }

    private void FixedUpdate()
    {
        // Move and turn the tank.
        Move();
        Turn();
    }

    private void Move()
    {
        if (Physics.Raycast(transform.position, -Vector3.up, k_GroundRayLength))
        {
            // Adjust the position of the tank based on the player's input.
            Vector3 movement = transform.forward * m_MovementInputValue * m_Force;

            m_Rigidbody.AddForce(movement);
        }
    }


    private void Turn()
    {
        // Adjust the rotation of the tank based on the player's input.
        float turn = m_TurnInputValue * m_TurnSpeed * Time.deltaTime;

        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);

        m_Rigidbody.MoveRotation(m_Rigidbody.rotation * turnRotation);
    }

    //  private void EngineAudio()
    //  {
    //      // Play the correct audio clip based on whether or not the tank is moving and what audio is currently playing.
    //if (Mathf.Abs (m_MovementInputValue) < 0.1f && Mathf.Abs (m_TurnInputValue) < 0.1f) 
    //{
    //	if (m_MovementAudio.clip == m_EngineDriving) 
    //	{
    //		m_MovementAudio.clip = m_EngineIdling;
    //		m_MovementAudio.pitch = Random.Range (m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
    //		m_MovementAudio.Play ();
    //	}
    //} 

    //else 
    //{
    //	if (m_MovementAudio.clip == m_EngineIdling) 
    //	{
    //		m_MovementAudio.clip = m_EngineDriving;
    //		m_MovementAudio.pitch = Random.Range (m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
    //		m_MovementAudio.Play ();
    //	}
    //}
    //  }
}
