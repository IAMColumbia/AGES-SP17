using UnityEngine;
using System.Collections;
using System;

public class SimpleCarController : MonoBehaviour {
    [SerializeField]
    private float maxSteerAngle = 30;

    [SerializeField]
    private float maxMotorTorque = 500;

    [SerializeField]
    private float brakeTorque = 50;

    [SerializeField]
    private WheelCollider[] wheelsUsedForSteering;

    [SerializeField]
    private WheelCollider[] wheelsUsedForDriving;

    [SerializeField]
    private WheelCollider[] allWheelColliders;

    private float steeringInput;
    private float driveInput;
    private Rigidbody rigidBody;

    // Use this for initialization
    private void Start ()
    {
        rigidBody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	private void Update ()
    {
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
            wheelsUsedForDriving[i].motorTorque = driveInput * maxMotorTorque;
        }



        float forwardVelocity = transform.InverseTransformDirection(rigidBody.velocity).z;
        for (int i = 0; i < allWheelColliders.Length; i++)
        {
            //TODO implement braking
            // if forwardVelocity matches input, then add motortorque.
            // If forwardVelocity is opposite of input, add brakeTorque.
        }
    }

    private void GetInput()
    {
        steeringInput = Input.GetAxis("Horizontal");
        driveInput = Input.GetAxis("Vertical");      
    }
}
