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
    private float brakeTorque = 400;
    [SerializeField]
    private float maxSpeedinMPH = 10;

    [SerializeField]
    private AnimationCurve torqueCurveModifier = new AnimationCurve(new Keyframe(0, 1), new Keyframe(20, 0.8f), new Keyframe(100, 0.3f));

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

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        getInput();
    }

    private void getInput()
    {
        driveInput = Input.GetAxis("Vertical");
        steeringInput = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        updateSteering();
        updateMotorTorque();
        updateBrakeTorque();
        updateWheelModels();
    }

    private void updateWheelModels()
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

    private void updateBrakeTorque()
    {
        //brakes?
        //when our forward velocity is one direction, and our input is the opposite direction,
        //apply the brakes

        bool carIsMovingSameDirectionAsInput = (ForwardVelocity > 0) == (driveInput > 0);

        float brakeTorqueToApply = 0;

        if (!carIsMovingSameDirectionAsInput && driveInput !=0)
        {
            brakeTorqueToApply = brakeTorque;
        }
        for (int i = 0; i < allWheelColliders.Length; i++)
        {
            allWheelColliders[i].brakeTorque = brakeTorqueToApply;
        }
    }

    private void updateMotorTorque()
    {
        for (int i = 0; i < wheelsUsedForDriving.Length; i++)
        {
            wheelsUsedForDriving[i].motorTorque = maxMotorTorque * driveInput * torqueCurveModifier.Evaluate(rigidBody.velocity.magnitude);
        }

        capSpeed();
    }

    private void capSpeed()
    {
        const float milesPerHourConst = 2.23694f;
        float speedInMPH = rigidBody.velocity.magnitude * milesPerHourConst;
        if (speedInMPH > maxSpeedinMPH)
        {
            rigidBody.velocity = (maxSpeedinMPH / milesPerHourConst) * rigidBody.velocity.normalized;
        }
    }

    private void updateSteering()
    {
        for (int i = 0; i < wheelsUsedForSteering.Length; i++)
        {
            wheelsUsedForSteering[i].steerAngle = maxSteeringAngle * steeringInput;
        }
    }
}
