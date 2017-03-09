using UnityEngine;
using System.Collections;
using System;

public class FootSoldierController : MonoBehaviour 
{
    public Player ControllingPlayer { get; set; }
    private bool IsInTank { get; set; }
    private new Rigidbody rigidbody;
    float xInput;
    float yInput;

    private void Start()
    {
        ControllingPlayer = new Player(1);
        rigidbody = GetComponent<Rigidbody>();
    }

    private void GetInput()
    {
        if (!IsInTank)
        {
            xInput = Input.GetAxis("HorizontalP" + ControllingPlayer.PlayerNumber);
            yInput = Input.GetAxis("VerticalP" + ControllingPlayer.PlayerNumber);
        }
    }

    private void Update()
    {
        GetInput();
    }

    private void FixedUpdate()
    {
        UpdateMovement();
        UpdateRotation();

    }

    private void UpdateRotation()
    {
        Vector3 moveDirection = new Vector3(xInput, 0, yInput);
       
  
      //  Debug.Log(moveDirection);
        Debug.DrawRay(rigidbody.position, moveDirection * 3, Color.red);

        float turnThreshold = 0.1f;
        if (moveDirection.magnitude > turnThreshold)
        {
            Quaternion newRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = newRotation;
        }
        // TODO: get vector 3 pointed in direction of input
        // then use
        // Quaternion.LookRotation(
    }

    private void UpdateMovement()
    {
        Vector3 moveDirection = new Vector3(xInput, 0, yInput);
        float speed = 3;
        rigidbody.MovePosition(rigidbody.position + (moveDirection * speed * Time.deltaTime));
    }
}
