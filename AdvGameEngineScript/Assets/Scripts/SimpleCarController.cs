using UnityEngine;
using System.Collections;
using System;

public class SimpleCarController : MonoBehaviour {

    // Use this for initialization


    [SerializeField]
    float maxSteerAngle = 30;

    [SerializeField]
    float motorTorque = 500;
    [SerializeField]
    float brakeTorque = 50;
    [SerializeField]
    WheelCollider[] wheelsUsedForSteering; //Ask why he'd call it that and no wheelColliders

    [SerializeField]
    WheelCollider[] wheelsUsedForDriving;

    [SerializeField]
    WheelCollider[] allWheelColliders;

    float steeringInput;
    float driveInput;
    Rigidbody rigidBody;

    void Start () {
        rigidBody = GetComponent<Rigidbody>();
	}
    void Update()
    {
        getInput();
    }
    void FixedUpdate()
    {
       
        for (int i = 0; i < wheelsUsedForSteering.Length; i++)
        {
            wheelsUsedForSteering[i].steerAngle = steeringInput * maxSteerAngle;
        }

        for (int i = 0; i < wheelsUsedForDriving.Length; i++)
        {
            wheelsUsedForDriving[i].motorTorque = driveInput * motorTorque;
        }
        float forwardVelocity = transform.InverseTransformDirection(rigidBody.velocity).z;
        for (int i = 0; i < allWheelColliders.Length; i++)
        {
            //TODO add brake inputs
            //TODO if forwardVelocit matches inut, then add motortorque
            //if forwardVelocity is opposite of input, add brakeTorque
        }
    }
    void getInput()
    {
        steeringInput = Input.GetAxis("Horizontal");
        driveInput = Input.GetAxis("Vertical");
        // float yInput = Input.GetAxis("Vertical"); We wouldn't use his for driving I guess
    }

    // Update is called once per frame
   
}
