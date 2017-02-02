using UnityEngine;
using System.Collections;
using System;

public class SimpleCarController : MonoBehaviour {
    [SerializeField]
    private float maxSteerAngle = 30;

    [SerializeField]
    private WheelCollider[] wheelsUsedForSteering;

    [SerializeField]
    private WheelCollider[] wheelsUsedForDriving;

    [SerializeField]
    private WheelCollider[] allWheelColliders;

    [SerializeField]
    private float maxMotorTorque = 500;

    [SerializeField]
    private float brakeTorque = 50;

    private float steeringInput;
    private float drivingInput;
    private Rigidbody rigidBody;

	// Use this for initialization
	void Start () {

        rigidBody = GetComponent<Rigidbody>();

	}
	
	// Update is called once per frame
	void Update () {
        GetInput();
	}

    private void FixedUpdate()
    {

        for (int i = 0; i < wheelsUsedForSteering.Length; i++)
        {
            wheelsUsedForSteering[i].steerAngle = steeringInput * maxSteerAngle;
        }

        for (int i = 0; i < wheelsUsedForDriving.Length; i++)
        {
            wheelsUsedForDriving[i].motorTorque = drivingInput * maxMotorTorque;
        }

        float forwardVelocity = transform.InverseTransformDirection(rigidBody.velocity).z;

        for (int i = 0; i < allWheelColliders.Length; i++)
        {
            //allWheelColliders[i].motorTorque = brakeTorque
                //TODO implement braking
                //if forward velocity matches input, then add motor torque, else, add brakeTorque
        }

    }

    private void GetInput()
    {
        steeringInput = Input.GetAxis("Horizontal");
        drivingInput = Input.GetAxis("Vertical");
    }
}
