using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float speed;
    [SerializeField]
    float gravity;
    [SerializeField]
    float jumpHeight;
    [SerializeField]
    CharacterController controller;
    [SerializeField]
    Rigidbody playerRB;
    [SerializeField]
    Transform originalObject;

    private Vector3 moveDirection = Vector3.zero;

    void Start()
    {
        // Store reference to attached component
        controller = GetComponent<CharacterController>();
        playerRB = GetComponent<Rigidbody>();
        originalObject = GetComponent<Transform>();
    }

    void Update()
    {
        // Character is on ground (built-in functionality of Character Controller)
        if (controller.isGrounded)
        {
            // Use input up and down for direction, multiplied by speed
            moveDirection = new Vector3(Input.GetAxis("Horizontal"),
                Input.GetAxis("Jump"), Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
        }
        // Apply gravity manually.
        moveDirection.y -= gravity * Time.deltaTime;
        // Move Character Controller
        controller.Move(moveDirection * Time.deltaTime);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Player")
        {
            playerRB.AddRelativeForce(Vector3.Reflect(originalObject.position, originalObject.position));
        }
    }
}