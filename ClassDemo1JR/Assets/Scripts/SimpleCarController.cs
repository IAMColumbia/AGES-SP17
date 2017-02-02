using UnityEngine;
using System.Collections;
using System;

public class SimpleCarController : MonoBehaviour
{

    [SerializeField]
    float maxSteerAngle = 30;

    [SerializeField]
    float maxMotorTorque = 500;

    [SerializeField]
    float brakeTorque = 50;

    [SerializeField]
    private WheelCollider[] wheelsUsedForSteering;

    [SerializeField]
    private WheelCollider[] wheelsUsedForDriving;

    [SerializeField]
    private WheelCollider[] allWheelColliders;
    
    float steeringInput;
    float driveInput;
    Rigidbody rigidBody;

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

    void FixedUpdate()
    {
        for (int i = 0; i < wheelsUsedForDriving.Length; i++)
        {
            wheelsUsedForDriving[i].motorTorque = driveInput * maxMotorTorque;
        }

        float forwardVelocity = transform.InverseTransformDirection(rigidBody.velocity).z;
        for (int i = 0; i < wheelsUsedForSteering.Length; i++)
        {
            wheelsUsedForSteering[i].steerAngle = steeringInput * maxSteerAngle;
        }

        for (int i = 0; i < allWheelColliders.Length; i++)
        {
            //TODO implement breaking
            //if forwardVelocity matches input, then add motorTorque
            //if forwardVelocity is opposite of input, then breakTorque
        }
    }

    void GetInput()
    {
        steeringInput = Input.GetAxis("Horizontal");
        driveInput = Input.GetAxis("Vertical");
    }
}
