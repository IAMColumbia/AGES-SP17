using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
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
    [SerializeField]
    CharacterController controller;
    [SerializeField]
    Rigidbody playerRB;

    //private Vector3 moveDirection = Vector3.zero;
    private Transform originalObject;

    private float currentSpeed;
    private bool isGrounded = true;

    void Start()
    {
        // Store reference to attached component
        controller = GetComponent<CharacterController>();
        playerRB = GetComponent<Rigidbody>();
        originalObject = GetComponent<Transform>();
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
        // Character is on ground (built-in functionality of Character Controller)
        if (controller.isGrounded)
        {
            // Use input up and down for direction, multiplied by speed
            //moveDirection = new Vector3(Input.GetAxis("Horizontal"),
            //    Input.GetAxis("Jump"), Input.GetAxis("Vertical"));
            //moveDirection = transform.TransformDirection(moveDirection);
            //moveDirection *= speed;
        }

        // Apply gravity manually.
        //moveDirection.y -= gravity * Time.deltaTime;

        // Move Character Controller
        //controller.Move(moveDirection * Time.deltaTime);

        //movement rotation
        playerRB.transform.Rotate(0, Input.GetAxis("Rotate") * rotationSpeed * Time.deltaTime, 0);

        //Movement forward and backward
        if(Input.GetAxis("Vertical") > 0)
            playerRB.AddRelativeForce(Vector3.forward * speed);
        else if(Input.GetAxis("Vertical") < 0)
            playerRB.AddRelativeForce(Vector3.forward * -speed);
    }

    void Jump()
    {
        if (Input.GetAxis("Jump") > 0 && isGrounded)
            playerRB.velocity = jumpHeight * Vector3.up;
            //playerRB.AddForce(Vector3.up * jumpHeight);

        //if (Input.GetKeyDown("space") && isGrounded)
        //    playerRB.velocity = jumpHeight * Vector3.up;
    }

    void Charge()
    {
        if(Input.GetAxis("Charge") > 0)
        {
            //playerRB.AddRelativeForce(Vector3.forward * chargeSpeed * speed);
            playerRB.AddRelativeForce(transform.forward * chargeSpeed * speed);
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