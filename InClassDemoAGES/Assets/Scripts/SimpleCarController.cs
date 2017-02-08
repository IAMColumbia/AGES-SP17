﻿using UnityEngine;
using System.Collections;
using System;

public class SimpleCarController : MonoBehaviour
{
    [SerializeField]
    private float maxSteerAngle = 30;

    [SerializeField]
    private float maxMotorTorque = 500;

    [SerializeField]
    private WheelCollider[] wheelsUsedForSteering;

    [SerializeField]
    private WheelCollider[] wheelsUsedForDriving;

    [SerializeField]
    private float brakeTorque = 50;

    [SerializeField]
    private WheelCollider[] allWheelColliders;

    private float steeringInput;
    private float driveInput;
    private Rigidbody rigidBody;
    private float ForwardVelocity;

    // Use this for initialization
    void Start ()
    {
        rigidBody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        GetInput();
	}

    private void FixedUpdate()
    {
        float forwardVelocity = transform.InverseTransformDirection(rigidBody.velocity).z;

        float breakTorqueToApply = 0;

        

        for (int i = 0; i < wheelsUsedForSteering.Length; i++)
        {
            wheelsUsedForSteering[i].steerAngle = steeringInput * maxSteerAngle;
        }

        for (int i = 0; i < wheelsUsedForDriving.Length; i++)
        {
            wheelsUsedForDriving[i].motorTorque = driveInput * maxMotorTorque;
        }

        for (int i = 0; i < allWheelColliders.Length; i++)
        {
            allWheelColliders[i].motorTorque = driveInput * 
            // TODO implement braking
            //if forwardVelocity matches input, then add motortorque
            //if forwardVelocity is opposite of input, add brakeTorque.
        }
    }

    private void GetInput()
    {
        driveInput = Input.GetAxis("Vertical");
        steeringInput = Input.GetAxis("Horizontal");
    }
}
