﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Rigidbody))]
public class TankJump : MonoBehaviour
{
    #region SerializedFields
    [SerializeField]
    private string jumpButton;
    [SerializeField]
    private float jumpSpeed;
    [SerializeField]
    private Transform groundDetectorPosition;
    [SerializeField]
    private Vector3 groundDetectorHalfExtents;
    [SerializeField]
    private LayerMask whatIsGround;
    [SerializeField]
    private AudioSource jumpSound;
    #endregion

    private Rigidbody myRigidBody;
    private bool isOnGround;

    private float xConst = 0;
    private float yConst = 0;
    private float zConst = 0;

    // Use this for initialization
    void Start ()
    {
        myRigidBody = GetComponent<Rigidbody>();

	}
	
	// Update is called once per frame
	void Update ()
    {
        UpdateIsOnGround();
        HandleJump();
	}

    private void HandleJump()
    {
        if (Input.GetButtonDown(jumpButton) && isOnGround == true)
        {
            jumpSound.Play();
            myRigidBody.AddRelativeForce(xConst,jumpSpeed,zConst);
        }
    }

    private void UpdateIsOnGround()
    {
        Collider[] groundObjectsArray = Physics.OverlapBox(groundDetectorPosition.position, groundDetectorHalfExtents, groundDetectorPosition.rotation, whatIsGround);

        isOnGround = (groundObjectsArray.Length > 0);
    }
}
