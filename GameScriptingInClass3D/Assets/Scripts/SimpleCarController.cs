using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCarController : MonoBehaviour {
    #region SerializedFields

    [SerializeField]
    float maxSteeringAngle = 40f;
    [SerializeField]
    float maxMotorTorque = 500f;
    [SerializeField]
    float brakeTorque = 50f;
    [SerializeField]
    WheelCollider[] wheelsUsedForSteering;
    [SerializeField]
    WheelCollider[] wheelsUsedForDriving;
    [SerializeField]
    WheelCollider[] wheelsAll;
    #endregion

    #region Private variables

    float steeringInput;
    float driveInput;
    Rigidbody rigidBody;
    #endregion

    void Start() {
        try { rigidBody = GetComponent<Rigidbody>(); }
        catch (Exception) { throw new Exception("SimpleCarController must be attached to a GameObject with a Rigidbody component."); }
    }

    #region UpdateFunctions

    void Update () {
        GetInput();
	}

    void FixedUpdate() {
        for (int i = 0; i < wheelsUsedForSteering.Length; i++)
            wheelsUsedForSteering[i].steerAngle = steeringInput * maxSteeringAngle;
        for (int i = 0; i < wheelsUsedForDriving.Length; i++)
            wheelsUsedForDriving[i].motorTorque = driveInput * maxMotorTorque;
        
    //TODO Fix braking logic
        //Get forwardVelocity of current motion in LOCAL space instead of GLOBAL space
        //float forwardVelocity = rigidBody.transform.InverseTransformDirection(rigidBody.velocity).z;
        //if(Math.Sign(forwardVelocity) == Math.Sign(driveInput))
        //    for (int i = 0; i < wheelsAll.Length; i++)
        //        wheelsAll[i].brakeTorque = driveInput * brakeTorque;

    }
    #endregion

    void GetInput() {
        steeringInput = Input.GetAxis("Horizontal");
        driveInput = Input.GetAxis("Vertical");
    }
}
