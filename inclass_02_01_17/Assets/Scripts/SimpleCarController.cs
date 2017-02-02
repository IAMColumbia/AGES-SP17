using UnityEngine;
using System.Collections;
using System;

public class SimpleCarController : MonoBehaviour {

    [SerializeField]
    private WheelCollider[] steeringWheels;

    [SerializeField]
    private WheelCollider[] driveWheels;

    private WheelCollider[] allWheelColliders;

    [SerializeField]
    private float maxSteerAngle = 30;

    [SerializeField]
    private float motorTorque = 500;

    [SerializeField]
    private float brakeTorque = 1500;

    private float steeringInput = 0;
    private float accelerationInput = 0;
    private float brakingInput = 0;

    private Rigidbody rb;

	// Use this for initialization
	private void Start () {
        allWheelColliders = GetComponentsInChildren<WheelCollider>();
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	private void Update () {
        GetInput();
	}

    private void FixedUpdate()
    {
        foreach(WheelCollider wheel in steeringWheels)
        {
            wheel.steerAngle = maxSteerAngle * steeringInput;
        }

        foreach(WheelCollider wheel in driveWheels)
        {
            wheel.motorTorque = motorTorque * accelerationInput;
        }

        foreach(WheelCollider wheel in allWheelColliders)
        {
            wheel.brakeTorque = brakeTorque * brakingInput;
        }
    }

    private void GetInput()
    {
        steeringInput = Input.GetAxis("Horizontal");
        accelerationInput = Input.GetAxis("Vertical");

        float forwardComponentOfVelocity = Vector3.Dot(transform.forward, rb.velocity);

        brakingInput = 0;

        if(accelerationInput >= 0)
        {
            if(forwardComponentOfVelocity < 0)
            {
                brakingInput = accelerationInput;
                accelerationInput = 0;
            }
        }
        else
        {
            if(forwardComponentOfVelocity >= 0)
            {
                brakingInput = -accelerationInput;
                accelerationInput = 0;
            }
        }

        //brakingInput = Input.GetAxis("Brake");
    }
}
