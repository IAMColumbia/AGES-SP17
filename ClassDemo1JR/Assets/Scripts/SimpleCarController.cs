using UnityEngine;
using System.Collections;
using System;

public class SimpleCarController : MonoBehaviour
{

    [SerializeField]
    float maxSteerAngle = 30;

    [SerializeField]
    float maxMotorTorque = 500;

    [SerializeField]
    float brakeTorque = 50;

    [SerializeField]
    private WheelCollider[] wheelsUsedForSteering;

    [SerializeField]
    private WheelCollider[] wheelsUsedForDriving;

    [SerializeField]
    private WheelCollider[] allWheelColliders;

    [SerializeField]
    private Transform[] allWheelModels;

    [SerializeField]
    private AnimationCurve torqueCurveModifier = new AnimationCurve(new Keyframe(0, 1), new Keyframe(100, 0.25f));

    [SerializeField]
    float maxSpeed = 50;

    float steeringInput;
    float driveInput;
    Rigidbody rigidBody;

    private float ForwardVelocity
    {
        get
        {
            return transform.InverseTransformDirection(rigidBody.velocity).z;
        }
    }


    // Use this for initialization
    void Start ()
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
        UpdateSteering();

        UpdateMotorTorque();

        UpdateWheelModels();
        float forwardVelocity = transform.InverseTransformDirection(rigidBody.velocity).z;

        

        float breakTorqueToApply = 0;

        bool forwardVelocityIsSameDirectionAsInput = (ForwardVelocity > 0) == (driveInput > 0);

        if (!forwardVelocityIsSameDirectionAsInput && driveInput != 0)
            breakTorqueToApply = brakeTorque;

        for (int i = 0; i < allWheelColliders.Length; i++)
        {
            allWheelColliders[i].brakeTorque = breakTorqueToApply;
            //TODO implement breaking
            //if forwardVelocity matches input, then add motorTorque
            //if forwardVelocity is opposite of input, then breakTorque

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

    private void UpdateSteering()
    {
        float curveMod = torqueCurveModifier.Evaluate(rigidBody.velocity.magnitude);
        for (int i = 0; i < wheelsUsedForDriving.Length; i++)
        {
            wheelsUsedForDriving[i].motorTorque = driveInput * maxMotorTorque * curveMod;
        }
    }

    private void UpdateMotorTorque()
    {
        for (int i = 0; i < wheelsUsedForSteering.Length; i++)
        {
            wheelsUsedForSteering[i].steerAngle = steeringInput * maxSteerAngle;
        }
    }

    private void CapSpeed()
    {
        const float mPHConversion = 2.23693629f;

        float currentSpeedInMPH = rigidBody.velocity.magnitude * mPHConversion;
        if (currentSpeedInMPH > maxSpeed)
        {
            rigidBody.velocity = (currentSpeedInMPH / mPHConversion) * rigidBody.velocity.normalized;

        }
        Debug.Log("Current Speed: " + currentSpeedInMPH);
    }

    void GetInput()
    {
        steeringInput = Input.GetAxis("Horizontal");
        driveInput = Input.GetAxis("Vertical");
    }
}
