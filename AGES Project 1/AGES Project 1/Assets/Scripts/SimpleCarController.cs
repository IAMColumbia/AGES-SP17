using UnityEngine;
using System.Collections;
using System;

public class SimpleCarController : MonoBehaviour
{
    [SerializeField]
    private float maxSteerAngle = 30;

    [SerializeField]
    private WheelCollider[] wheelsUsedForSteering;

    [SerializeField]
    private WheelCollider[] wheelsUsedForDriving;

    [SerializeField]
    private WheelCollider[] allWheelColliders;

    [SerializeField]
    private AnimationCurve torqueCurveModifier = new AnimationCurve(new Keyframe(0, 1), new Keyframe(100, 0.25f));

    [SerializeField]
    private float maxSpeedInMPH = 7;

    [SerializeField]
    private Transform[] allWheelModels;

    [SerializeField]
    private float brakeTorque = 50;

    [SerializeField]
    private float maxMotorTorque = 500;

	[SerializeField]
	string horizontalAxisInput = "Horizontal_GreenCar";

	[SerializeField]
	string verticalAxisInput = "Vertical_GreenCar";

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
        const float mphConversion = 2.23693629f;

        float currentSpeedInMPH = rigidBody.velocity.magnitude * mphConversion;

        if(currentSpeedInMPH > maxSpeedInMPH)
        {
            rigidBody.velocity = (maxSpeedInMPH / mphConversion) * rigidBody.velocity.normalized;
        }

        //Debug.Log("Current Speed: " + currentSpeedInMPH);
    }

    private void UpdateWheelModels()
    {
        for (int i = 0; i < allWheelModels.Length; i++)
        {
            Vector3 positionToSet;
            Quaternion rotationToSet;

            allWheelColliders[i].GetWorldPose(out positionToSet, out rotationToSet);

            //allWheelModels[i].position = positionToSet;
            //allWheelModels[i].rotation = rotationToSet;
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
            //TODO implement braking
            //if forward velocity matches input, then add motorTorque.
            //if forward velocity is opposite of input, then add brakeTorque.3
        }
    }

    private void UpdateMotorTorque()
    {
        float curveMod = torqueCurveModifier.Evaluate(rigidBody.velocity.magnitude);

        //Debug.Log("Curent CurveMod: " + curveMod);

        for (int i = 0; i < wheelsUsedForDriving.Length; i++)
        {
            wheelsUsedForDriving[i].motorTorque = driveInput * maxMotorTorque * curveMod;
            //Debug.Log(this.name + "Motor torque" + wheelsUsedForDriving[i].motorTorque);
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
		steeringInput = Input.GetAxis(horizontalAxisInput);
		driveInput = Input.GetAxis(verticalAxisInput);
    }
}
