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
    Vector3 moveDirection;
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

    private void ConvertInputToCameraRelative()
    {
        moveDirection = new Vector3(yInput, 0, -xInput);
        moveDirection = Camera.main.transform.InverseTransformDirection(moveDirection);
    }

    private void Update()
    {
        GetInput();
    }

    private void FixedUpdate()
    {
        ConvertInputToCameraRelative();
      //  UpdateMovement();
        UpdateRotation();

    }

    private void UpdateRotation()
    {     
        // Debug.DrawRay(rigidbody.position, moveDirection * 3, Color.red);

        float turnThreshold = 0.1f;
        if (moveDirection.magnitude > turnThreshold)
        {
            Quaternion newRotation = Quaternion.LookRotation(moveDirection);
            newRotation.eulerAngles = new Vector3(0, newRotation.eulerAngles.y, 0);
            transform.rotation = newRotation;
        }
    }

    private void UpdateMovement()
    {
        float speed = 3;
        rigidbody.MovePosition(rigidbody.position + (moveDirection * speed * Time.deltaTime));
    }
}
