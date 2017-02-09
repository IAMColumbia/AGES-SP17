using UnityEngine;
using System.Collections;
using System;

public class SimpleCarController : MonoBehaviour {

    [SerializeField]
    private float maxSteerAngle = 30;

    [SerializeField]
    private AnimationCurve torqueCurveModifier = new AnimationCurve(new Keyframe(0, 1), new Keyframe(100, 0.25f));

    [SerializeField]
    private float maxMotorTorque = 500;

    [SerializeField]
    private float brakeTorque = 50;

    [SerializeField]
    private float maxSpeedInMPH = 7;

    [SerializeField]
    private WheelCollider[] wheelsUsedForSteering;

    [SerializeField]
    private WheelCollider[] wheelsUsedForDriving;

    [SerializeField]
    private WheelCollider[] allWheelColliders;

    [SerializeField]
    private Transform[] allWheelModels;


    private float steeringInput;
    private float driveInput;
    private Rigidbody rigidBody;

    private float ForwardVelocity
    {
        get
        {
            return transform.InverseTransformDirection(rigidBody.velocity).z; 
        }
    }

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
        UpdateSteering();
        UpdateMotorTorque();
        CapSpeed();
        UpdateBrakeTorque();
        UpdateWheelModels();
    }

    private void CapSpeed()
    {
        const float milesPerHourConversion = 2.23693629f;

        float currentSpeedInMPH = rigidBody.velocity.magnitude * milesPerHourConversion;

        Debug.Log("Current Speed: " + currentSpeedInMPH);

        if (currentSpeedInMPH > maxSpeedInMPH)
        {
            rigidBody.velocity = ( maxSpeedInMPH / milesPerHourConversion) * rigidBody.velocity.normalized;
        }
    }

    private void UpdateWheelModels()
    {
        for (int i = 0; i < allWheelModels.Length; i++)
        {
            Vector3 positionToSet;
            Quaternion rotationToSet;

            allWheelColliders[i].GetWorldPose(out positionToSet, out rotationToSet);
               
            allWheelModels[i].position = positionToSet;
            allWheelModels[i].rotation = rotationToSet; 
        }
    }

    private void UpdateBrakeTorque()
    {
        float brakeTorqueToApply = 0;

        bool forwardVelocityIsSameDirectionAsInput = (ForwardVelocity > 0) == (driveInput > 0);

        if (!forwardVelocityIsSameDirectionAsInput && driveInput != 0)
        {
            brakeTorqueToApply = brakeTorque;
        }

        for (int i = 0; i < allWheelColliders.Length; i++)
        {
            allWheelColliders[i].brakeTorque = brakeTorqueToApply;
        }
    }

    private void UpdateMotorTorque()
    {
        float curveModifier = torqueCurveModifier.Evaluate(rigidBody.velocity.magnitude);

        //Debug.Log("Current curve modifier :" + curveModifier);

        for (int i = 0; i < wheelsUsedForDriving.Length; i++)
        {
            wheelsUsedForDriving[i].motorTorque = driveInput * maxMotorTorque * curveModifier;
        }
    }

    private void UpdateSteering()
    {
        for (int i = 0; i < wheelsUsedForSteering.Length; i++)
        {
            wheelsUsedForSteering[i].steerAngle = steeringInput * maxSteerAngle;
        }
    }

    private void GetInput()
    {
        steeringInput = Input.GetAxis("Horizontal");
        driveInput = Input.GetAxis("Vertical");
    }
}
