using UnityEngine;
using System.Collections;
using System;

public class SimpleCarController : MonoBehaviour
{
    [SerializeField]
    private float maxSteeringAngle = 30;
    [SerializeField]
    private float maxSpeedInMPH = 120;
    [SerializeField]
    private float maxMotorTorque = 400;
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
    private Transform[] allWheelModels;

    private float driveInput;
    private float steeringInput;
    private Rigidbody rigidBody;

    private float ForwardVelocity
    {
        get
        {
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
        driveInput = Input.GetAxis("Vertical");
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
        //Brakes?
        // When our forward velocity is one direction, and our input is the opposite direction,
        // apply the brakes!
        bool carIsMovingSameDirectionAsInput = (ForwardVelocity > 0) == (driveInput > 0);
        float brakeTorqueToApply = 0;

        if (!carIsMovingSameDirectionAsInput && driveInput != 0)
            brakeTorqueToApply = brakeTorque;

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
                maxMotorTorque * driveInput * torqueCurveModifier.Evaluate(rigidBody.velocity.magnitude);
        }

        CapSpeed();
    }

    private void CapSpeed()
    {
        const float milesPerHourConversion = 2.23964f;

        float speedInMPH = rigidBody.velocity.magnitude * milesPerHourConversion;

        if (speedInMPH > maxSpeedInMPH)
        {
            rigidBody.velocity = (maxSpeedInMPH / milesPerHourConversion) * rigidBody.velocity.normalized;
        }
    }

    private void UpdateSteering()
    {
        for (int i = 0; i < wheelsUsedForSteering.Length; i++)
        {
            wheelsUsedForSteering[i].steerAngle = maxSteeringAngle * steeringInput;
        }
    }
}
