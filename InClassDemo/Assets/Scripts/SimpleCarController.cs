using UnityEngine;
using System.Collections;
using System;

public class SimpleCarController : MonoBehaviour
{
    [SerializeField]
    private float maxSteeringAngle = 30;

    [SerializeField]
    private float maxMotorTorque = 400;

    [SerializeField]
    float maxSpeedInMPH = 10;

    [SerializeField]
    private AnimationCurve torqueCurveModifier = 
        new AnimationCurve(new Keyframe(0, 1), new Keyframe(20, 0.8f), new Keyframe(100, 0.3f));

    [SerializeField]
    private float brakeTorque = 400;

    [SerializeField]
    private WheelCollider[] wheelsUsedForSteering;

    [SerializeField]
    private WheelCollider[] wheelsUsedForDriving;

    [SerializeField]
    private WheelCollider[] allWheelColliders;

    [SerializeField]
    Transform[] allWheelModels;

    private float driverInput;
    private float steeringInput;
    private Rigidbody rigidBody;

    private float ForwardVelocity
    {
        get
        {
            //next line changes rigidbody to use local directions (forward, back, etc)
            return transform.InverseTransformDirection(rigidBody.velocity).z;
        }
    }

    // Use this for initialization
    private void Awake ()
    {
        rigidBody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	private void Update ()
    {
        GetInput();

    }

    private void GetInput()
    {
        driverInput = Input.GetAxis("Vertical");
        steeringInput = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        UpdateSteering();
        UpdateMotorTorque();
        UpdateBrakeTorque();
        UpdateWheelModels();
    }

    private void UpdateWheelModels()
    {
        Vector3 positionToSet;
        Quaternion rotationToSet;

        for (int i = 0; i < allWheelModels.Length; i++)
        {
            allWheelColliders[i].GetWorldPose(out positionToSet, out rotationToSet);
            allWheelModels[i].position = positionToSet;
            allWheelModels[i].rotation = rotationToSet;
        }
    }

    private void UpdateBrakeTorque()
    {
        // Brakes:
        //When our forward velocity is one direction and our input is the opposite direction,
        //apply the brakes.

        //next line changes rigidbody to use local directions (forward, back, etc)
        //float forwardVelocity = transform.InverseTransformDirection(rigidBody.velocity).z;

        //if forwardvelocity is more than 0 and driverinput is more than 0 bool is true.
        bool carIsMovingSameDirectionAsInput = (ForwardVelocity > 0) == (driverInput > 0);

        float brakeTorqueToApply = 0;

        if (!carIsMovingSameDirectionAsInput && driverInput != 0)
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
        for (int i = 0; i < wheelsUsedForDriving.Length; i++)
        {
            wheelsUsedForDriving[i].motorTorque = 
                maxMotorTorque * driverInput * torqueCurveModifier.Evaluate(rigidBody.velocity.magnitude);
        }
        //Debug.Log("Current torqueCurveMod: " + torqueCurveModifier.Evaluate(rigidBody.velocity.magnitude));

        CapSpeed();
    }

    private void CapSpeed()
    {
        const float MilesPerHourConst = 2.23694f;
        float speedInMPH = rigidBody.velocity.magnitude * MilesPerHourConst;
        if (speedInMPH > maxSpeedInMPH)
        {
            rigidBody.velocity = (maxSpeedInMPH / MilesPerHourConst) * rigidBody.velocity.normalized;
        }
        Debug.Log("Speed in MPH: " + speedInMPH);
    }

    private void UpdateSteering()
    {
        for (int i = 0; i < wheelsUsedForSteering.Length; i++)
        {
            wheelsUsedForSteering[i].steerAngle = maxSteeringAngle * steeringInput;
        }
    }
}
