using UnityEngine;
using System.Collections;
using System;

public class SimpleCarControllerFromScratch : MonoBehaviour 
{
    [SerializeField]
    float maxSteerAngle = 30;
    [SerializeField]
    float maxMotorTorque = 500;
    [SerializeField]
    float maxSpeedInMPH = 100;
    [SerializeField]
    float brakeTorque = 1000;
    [SerializeField]
    float reverseTorque = 200;
    [SerializeField]
    AnimationCurve torqueCurveModifier = new AnimationCurve(new Keyframe(0, 1), new Keyframe(10, 0.75f), new Keyframe(100, 0.1f));

    [SerializeField]
    WheelCollider[] wheelsConnectedToSteering;

    [SerializeField]
    WheelCollider[] wheelsConnectedToDriving;

    [SerializeField]
    WheelCollider[] allWheels;

    [SerializeField]
    Transform[] allWheelModels;



    float steeringInput;
    float driveInput;
    Rigidbody rigidBody;

    private bool IsInputSameDirectionAsVelocity
    {
        get
        {
            return (driveInput > 0) == (ForwardVelocity > 0);
        }
    }
    private float ForwardVelocity
    {
        get
        {
            return transform.InverseTransformDirection(rigidBody.velocity).z;
        }
    }

    // Use this for initialization
    void Awake () 
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
        HandleDriving();
        CapSpeed();
        HandleBrakes();
        HandleSteering();
        ApplyLocalPositionToWheelModels();
    }

    private void ApplyLocalPositionToWheelModels()
    {
        Vector3 positionToSet;
        Quaternion rotationToSet;

        for (int i = 0; i < allWheelModels.Length; i++)
        {
            allWheels[i].GetWorldPose(out positionToSet, out rotationToSet);
            allWheelModels[i].position = positionToSet;
            allWheelModels[i].rotation = rotationToSet;
        }
    }

    private void HandleSteering()
    {
        for (int i = 0; i < wheelsConnectedToSteering.Length; i++)
        {
            wheelsConnectedToSteering[i].steerAngle = maxSteerAngle * steeringInput;
        }
    }

    void HandleDriving()
    {
        if (IsInputSameDirectionAsVelocity || ForwardVelocity == 0)
        {
            float torqueToApply = maxMotorTorque;

            if (driveInput < 0)
            {
                torqueToApply = reverseTorque;
            }

            torqueToApply *= torqueCurveModifier.Evaluate(rigidBody.velocity.magnitude);
            //Debug.Log(torqueCurveModifier.Evaluate(rigidBody.velocity.magnitude));
            for (int i = 0; i < wheelsConnectedToDriving.Length; i++)
            {
                wheelsConnectedToDriving[i].motorTorque = torqueToApply * driveInput;
            }
        }
        else
        {
            for (int i = 0; i < wheelsConnectedToDriving.Length; i++)
            {
                wheelsConnectedToDriving[i].motorTorque = 0;
            }
        }
        
    }

    void HandleBrakes()
    {
        float brakeTorqueToApply = 0;
        if (!IsInputSameDirectionAsVelocity && ForwardVelocity != 0)
        {
            brakeTorqueToApply = brakeTorque;
        }

        for (int i = 0; i < allWheels.Length; i++)
        {
            allWheels[i].brakeTorque = brakeTorqueToApply * Mathf.Abs(driveInput);
        }
    }

    private void CapSpeed()
    {
        // one meter per second = 2.23693629 miles per hour
        const float mphConstant = 2.23693629f;

        float speedInMPH = rigidBody.velocity.magnitude * mphConstant;
        Debug.Log("MPH: " + speedInMPH);
        if (speedInMPH > maxSpeedInMPH)
        {
            // convert back to meters per second, then multiply by direction
            rigidBody.velocity = (maxSpeedInMPH / mphConstant) * rigidBody.velocity.normalized;
        }

    }


    private void OldCode()
    {
        float forwardVelocity = transform.InverseTransformDirection(rigidBody.velocity).z;
        bool isCarMovingForward = forwardVelocity > 0;

        // brakes
        bool isInputOppositeOfVelocity = (driveInput > 0) != (forwardVelocity > 0);

        if (isInputOppositeOfVelocity)
        {
            for (int i = 0; i < allWheels.Length; i++)
            {
                allWheels[i].brakeTorque = brakeTorque * -driveInput;
            }
        }
        else
        {
            for (int i = 0; i < allWheels.Length; i++)
            {
                allWheels[i].brakeTorque = 0;
            }
        }



        if (driveInput > 0)
        {
            for (int i = 0; i < wheelsConnectedToDriving.Length; i++)
            {
                wheelsConnectedToDriving[i].motorTorque = maxMotorTorque * driveInput;
            }

            for (int i = 0; i < wheelsConnectedToSteering.Length; i++)
            {
                wheelsConnectedToSteering[i].steerAngle = maxSteerAngle * steeringInput;
            }
        }
        else
        {


            if (isCarMovingForward)
            {

            }
            else
            {
                for (int i = 0; i < allWheels.Length; i++)
                {
                    allWheels[i].motorTorque = reverseTorque * driveInput;
                }
            }
        }
    }

    private void GetInput()
    {
        steeringInput = Input.GetAxis("Horizontal");
        driveInput = Input.GetAxis("Vertical");
    }
}
