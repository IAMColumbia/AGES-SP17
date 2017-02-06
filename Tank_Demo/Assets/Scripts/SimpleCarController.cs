using UnityEngine;
using System.Collections;
using System;

public class SimpleCarController : MonoBehaviour {

    [SerializeField]
    float maxSteeringAngle = 30;

    [SerializeField]
    float maxMotorTorque = 400;

    [SerializeField]
    float maxSpeedInMPH = 10;

    [SerializeField]
    private AnimationCurve torqueCurveModifier = 
        new AnimationCurve(new Keyframe(0, 1), new Keyframe(20, 0.8f), new Keyframe(100, 0.3f));

    [SerializeField]
    float brakeTorque = 400;

    [SerializeField]
    private WheelCollider[] wheelsUsedForSteering;

    [SerializeField]
    private WheelCollider[] wheelsUsedForDriving;

    [SerializeField]
    private WheelCollider[] allWheelColliders;

    [SerializeField]
    private Transform[] allWheelModels;

    float driveInput;
    float steeringInput;

    private Rigidbody rigidBody;

    float ForwardVelocity
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
    void Update()
    {
        GetInput();
    }


    private void FixedUpdate()
    {
        UpdateSteering();

        UpdateMotorTorque();

        UpdateBrakeTorque();

        UpdateWheelModels();
    }

    void UpdateWheelModels()
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

    void GetInput()
    {
        driveInput = Input.GetAxis("Vertical");
        steeringInput = Input.GetAxis("Horizontal");
    }

    void UpdateSteering()
    {
        for (int i = 0; i < wheelsUsedForSteering.Length; i++)
        {
            wheelsUsedForSteering[i].steerAngle = maxSteeringAngle * steeringInput;
        }
    }

    void UpdateMotorTorque()
    {
        for (int i = 0; i < wheelsUsedForDriving.Length; i++)
        {
            wheelsUsedForDriving[i].motorTorque = maxMotorTorque * driveInput 
                * torqueCurveModifier.Evaluate(rigidBody.velocity.magnitude);
        }
        //Debug.Log("Current torqueCurveMod: " + torqueCurveModifier.Evaluate(rigidBody.velocity.magnitude));

        CapSpeed();
    }

    private void CapSpeed()
    {
        const float milesPerHourConst = 2.23694f;

        float speedInMPH = rigidBody.velocity.magnitude * milesPerHourConst;

        if(speedInMPH > maxSpeedInMPH)
        {
            rigidBody.velocity = (maxSpeedInMPH / milesPerHourConst) * rigidBody.velocity.normalized;
        }
        Debug.Log("Speed in MPH " + speedInMPH);
    }

    void UpdateBrakeTorque()
    {
        float forwardVelocity = transform.InverseTransformDirection(rigidBody.velocity).z;

        bool carIsMovingInSameDirectionAsInput = (forwardVelocity > 0) == (driveInput > 0);

        float brakeTorqueToApply = 0f;

        if (!carIsMovingInSameDirectionAsInput && driveInput != 0)
        {
            brakeTorqueToApply = brakeTorque;
        }
        for (int i = 0; i < allWheelColliders.Length; i++)
        {
            allWheelColliders[i].brakeTorque = brakeTorqueToApply;
        }
    }

}
