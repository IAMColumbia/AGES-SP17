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

    [SerializeField]
    private float maxVelocityMPH = 20;

    [SerializeField]
    private AnimationCurve torqueCurve = new AnimationCurve(new Keyframe(0, 1), new Keyframe(5, 0.1f));

    private float steeringInput = 0;
    private float accelerationInput = 0;
    private float brakingInput = 0;

    float ForwardComponentOfVelocity {
        get {
            float answer = Vector3.Dot(transform.forward, rb.velocity);
            return answer;
        }
    }

    private Rigidbody rb;

    bool velocityDirectionOppositeOfAccelDirection;

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
        UpdateSteering();
        UpdateMotorTorque();
        UpdateBraking();
        CapSpeed();
    }

    private void GetInput()
    {
        steeringInput = Input.GetAxis("Horizontal");
        accelerationInput = Input.GetAxis("Vertical");
        brakingInput = Input.GetAxis("Brake");

        //if accel input and current velocity are in opposite directions, brake instead of applying acceleration
        if(accelerationInput > 0)
        {
            if(ForwardComponentOfVelocity < 0)
            {
                brakingInput = brakeTorque;
                accelerationInput = 0;
            }
        }
        else if (accelerationInput < 0)
        {
            if(ForwardComponentOfVelocity > 0)
            {
                brakingInput = brakeTorque;
                accelerationInput = 0;
            }
        }
    }

    private void UpdateMotorTorque()
    {
        foreach (WheelCollider wheel in driveWheels)
        {
            float torqueAdjust = torqueCurve.Evaluate(ForwardComponentOfVelocity);
            wheel.motorTorque = motorTorque * torqueAdjust * accelerationInput;
        }
    }

    private void UpdateSteering()
    {
        foreach (WheelCollider wheel in steeringWheels)
        {
            wheel.steerAngle = maxSteerAngle * steeringInput;
        }
    }

    private void UpdateBraking()
    {
        foreach (WheelCollider wheel in allWheelColliders)
        {
            wheel.brakeTorque = brakeTorque * brakingInput;
        }
    }

    private void CapSpeed()
    {
        const float mphConversion = 2.2f;
        if(rb.velocity.magnitude * mphConversion > maxVelocityMPH)
        {
            rb.velocity = rb.velocity.normalized * maxVelocityMPH / mphConversion;
        }
    }
}
