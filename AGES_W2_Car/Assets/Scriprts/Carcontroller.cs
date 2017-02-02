using UnityEngine;
using System.Collections;
using System;

public class Carcontroller : MonoBehaviour {

    [SerializeField]
    private float maxSteerAngle = 30;

    [SerializeField]
    private float maxMotorTouq = 300;

    [SerializeField]
    private WheelCollider[] wheelsUsedForSteering;

    [SerializeField]
    private WheelCollider[] wheelsUsedForDriving;


    [SerializeField]
    private float brakeTorque = 50;

    [SerializeField]
    private WheelCollider[] Allwheels;

    private float steeringinput;
    private float driveinput;
    private Rigidbody rigitBody;

    // Use this for initialization
    void Start ()
    {
        rigitBody = GetComponent<Rigidbody>();
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
            wheelsUsedForDriving[i].motorTorque = driveinput * maxMotorTouq;
        }

        float forwardVelocity = transform.InverseTransformDirection(rigitBody.velocity).z;
        for (int i = 0; i < Allwheels.Length; i++)
        {
          //TODO impliment breaking
          //TODO if forward velocety = input -> add mototorouq,
        }
    }

    private void GetInput()
    {
         steeringinput = Input.GetAxis("Horizontal");
        driveinput = Input.GetAxis("Vertical");
    }
}
