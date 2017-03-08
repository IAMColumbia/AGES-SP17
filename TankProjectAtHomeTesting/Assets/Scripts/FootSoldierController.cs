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
        // TODO: get vector 3 pointed in direction of input
        // then use
        // Quaternion.LookRotation(
    }

    private void UpdateMovement()
    {
        
    }
}
