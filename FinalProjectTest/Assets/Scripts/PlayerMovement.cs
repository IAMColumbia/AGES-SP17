using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float deadZoneMinimum;
    [SerializeField]
    float speed;
    [SerializeField]
    float rotationSpeed;
    [SerializeField]
    float gravity;
    [SerializeField]
    float jumpHeight;
    [SerializeField]
    float chargeSpeed;
    //[SerializeField]
    //CharacterController controller;
    [SerializeField]
    Rigidbody playerRB;

    //private Vector3 moveDirection = Vector3.zero;
    private Transform originalObject;

    private float currentSpeed;

    [HideInInspector]
    public bool isGrounded = true;

    public int playerNumber = 1;

    private string movementAxisName;
    private string rotateAxisName;
    private string chargeAxisName;
    private string jumpAxisName;

    void Start()
    {
        // Store reference to attached component
        //controller = GetComponent<CharacterController>();
        playerRB = GetComponent<Rigidbody>();
        originalObject = GetComponent<Transform>();

        //Set up multiplayer controls
        movementAxisName = "Vertical" + playerNumber;
        rotateAxisName = "Rotate" + playerNumber;
        chargeAxisName = "Charge" + playerNumber;
        jumpAxisName = "Jump" + playerNumber;
    }

    void Update()
    {
        Move();
        Jump();
        Charge();
        var vel = playerRB.velocity;
        currentSpeed = vel.magnitude;
    }
    
    private void FixedUpdate()
    {
        //Move();
        //Jump();
    }

    void Move()
    {
        //movement rotation
        playerRB.transform.Rotate(0, Input.GetAxis(rotateAxisName) * rotationSpeed * Time.deltaTime, 0);

        //Movement forward and backward
        if(Input.GetAxis(movementAxisName) > deadZoneMinimum)
            playerRB.AddRelativeForce(Vector3.forward * speed);
        else if(Input.GetAxis(movementAxisName) < deadZoneMinimum)
            playerRB.AddRelativeForce(Vector3.forward * -speed);
    }

    void Jump()
    {
        if (Input.GetAxis(jumpAxisName) > deadZoneMinimum && isGrounded)
            playerRB.velocity = jumpHeight * Vector3.up;
    }

    void Charge()
    {
        if(Input.GetAxis(chargeAxisName) > deadZoneMinimum)
        {
            playerRB.AddRelativeForce(Vector3.forward * chargeSpeed * speed);
            //playerRB.AddRelativeForce(transform.forward * chargeSpeed * speed);
        }
    }

    void OnCollisionEnter(Collision otherPlayerCollider)
    {
        Rigidbody otherPlayerRB = new Rigidbody();

        if (otherPlayerCollider.collider.tag == "Player")
        {
            otherPlayerRB.position = Vector3.Reflect(originalObject.position * currentSpeed,originalObject.position);            
            currentSpeed = 0;
        }

        if (otherPlayerCollider.collider.tag == "Ground")
        {
            Debug.Log("Player is touching the ground");
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
            isGrounded = false;
    }



    //Use below for killing player
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == "Death")
    //    {
    //        playerRB.gameObject.SetActive(false);
    //    }                
    //}
}