using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    public int m_PlayerNumber = 1;
    public float m_Force = 20f;
    public float m_TurnSpeed = 180f;
    public float m_DefaultMass = 1f;

    private string m_MovementAxisName;
    private string m_TurnAxisName;
    private Rigidbody m_Rigidbody;
    private float m_MovementInputValue;
    private float m_TurnInputValue;
    private const float k_GroundRayLength = 1f;

    private string m_ShieldXName;
    private string m_ShieldYName;
    private string m_BoostButton;
    public float m_BoostForce = 100f;
    public float m_BoostMass = 5;
    public bool Boosting = false;

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    void Start () {
        m_MovementAxisName = "Vertical" + m_PlayerNumber;
        m_TurnAxisName = "Horizontal" + m_PlayerNumber;
        m_BoostButton = "Boost" + m_PlayerNumber;
    }

    void Update () {
        m_MovementInputValue = Input.GetAxis(m_MovementAxisName);
        m_TurnInputValue = Input.GetAxis(m_TurnAxisName);
    }

    private void FixedUpdate()
    {
        Move();
        Turn();
        Boost();
    }

    private void Move()
    {
        if (Physics.Raycast(transform.position, -Vector3.up, k_GroundRayLength))
        {
            Vector3 movement = transform.forward * m_MovementInputValue * m_Force;

            m_Rigidbody.AddForce(movement);
        }
    }

    private void Turn()
    {
        float turn = m_TurnInputValue * m_TurnSpeed * Time.deltaTime;

        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);

        m_Rigidbody.MoveRotation(m_Rigidbody.rotation * turnRotation);
    }

    private void Boost()
    {
        if (Input.GetButton(m_BoostButton))
        {
            Boosting = true;

            Vector3 BoostMovement = transform.forward * m_BoostForce;

            m_Rigidbody.AddForce(BoostMovement);

            m_Rigidbody.mass = m_BoostMass;

            //m_BoostAudio.Play();
        }
        else
        {
            Boosting = false;

            m_Rigidbody.mass = m_DefaultMass;
        }
    }
}
