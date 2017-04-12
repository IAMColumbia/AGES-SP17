using UnityEngine;
using System.Collections;
using System;

public class PlayerFlightControl : MonoBehaviour {

    // Use this for initialization
    [SerializeField]
    float m_Speed = 12f;
    [SerializeField]
    float m_TurnSpeed = 10f;
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
   
    float m_MovementInputValue;
    float m_TurnInputValue;
    float m_OriginalPitch;
    
    //mvem
    float moveHorizontal;
    float moveVertical;
    /// <summary>
    //
    /// </summary>
	//void Start () {
       
 //   }
    void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        m_Rigidbody.isKinematic = false;
        m_MovementInputValue = 0f;
        m_TurnInputValue = 0f;
    }
    private void OnDisable()
    {
        m_Rigidbody.isKinematic = true;
    }
    // Update is called once per frame
    void Update () {
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");
    }
    void FixedUpdate()
    {
        Quaternion deltaRotation = Quaternion.Euler(eulerAngleVelocity * Time.deltaTime);
        m_Rigidbody = GetComponent<Rigidbody>();
        //rb is now m_Rigidbody
        m_Rigidbody.MoveRotation(m_Rigidbody.rotation * deltaRotation);

        Turn();
        //Move();
    }
    //private void Move()
    //{
    //    Vector3 alwaysMoving;

    //    alwaysMoving = Vector3.forward;
    //    transform.position = alwaysMoving;
    //    //for (int i = 0; i < Time.fixedTime; i++)
    //    //{
    //    //    while (i < Time.fixedTime)
    //    //    {
    //    //        transform.position = alwaysMoving;
    //    //    }
    //    //}
    //}
    private void Turn()
    {
        // Adjust the rotation of the tank based on the player's input.
        //Make all variables are set up 
        //left and right is turn 
        float moveHorizontal = m_TurnInputValue * m_TurnSpeed * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(moveHorizontal, 0f, 0f);
        m_Rigidbody.MoveRotation(m_Rigidbody.rotation * turnRotation);        
    }
}
