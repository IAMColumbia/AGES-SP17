using UnityEngine;
using System.Collections;
using System;

public class SimpleCarController : MonoBehaviour {

    [SerializeField]
    private float maxSteerAngle = 30;

    [SerializeField]
    private float maxMotorTorque = 500;

    [SerializeField]
    private float maxBrakeTorque;

    [SerializeField]
    private WheelCollider[]  wheelsUsedForSteering;

    [SerializeField]
    private WheelCollider[] wheelsUsedForDriving;

    [SerializeField]
    private WheelCollider[] wheelsUsedForBraking;


    private float steeringinput;
    private float drivinginput;
    //private float brakinginput;

    private Rigidbody bodyThatIsRigid;

    // Use this for initialization
    void Start ()
    {
        bodyThatIsRigid = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        GetInput();
	}

    private void FixedUpdate()
    {

        

        for (int i = 0; i < wheelsUsedForSteering.Length; i++)
        {
            wheelsUsedForSteering[i].steerAngle = steeringinput * maxSteerAngle;
        }

        for (int i = 0; i < wheelsUsedForDriving.Length; i++)
        {
            wheelsUsedForDriving[i].motorTorque = drivinginput * maxMotorTorque;
        }




        float forwardVelocity = transform.InverseTransformDirection(bodyThatIsRigid.velocity).z;
        for (int i = 0; i < wheelsUsedForBraking.Length; i++)
        {
            //if forward velocity matches input, then add motor torque
            //if forward velocity doesnt match input, then add brake torque
        }

    }

    private void GetInput()
    {
        steeringinput = Input.GetAxis("Horizontal");
        drivinginput = Input.GetAxis("Vertical");
        //brakinginput = Input.GetAxis("Jump");
    }
}
