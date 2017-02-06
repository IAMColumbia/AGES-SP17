using UnityEngine;
using System.Collections;
using System;

public class TankMovement : MonoBehaviour
{
    [SerializeField] float maxSteeringAngle = 30f;
    [SerializeField] float maxMotorTorque = 400f;
    [SerializeField] float brakeSpeedMultiplier, brakeTorque = 10f;
    [SerializeField] WheelCollider[] wheelsUsedForSteering, wheelsUsedForDriving, allWheelColliders;
    float driveInput, steeringInput;
    [SerializeField] float maxSpeedInMPH = 10;

    [SerializeField] Transform[] allWheelModels;

    //new Keyframe(StartAmount, time)
    [SerializeField] private AnimationCurve torqueCurveModifier =
        new AnimationCurve(new Keyframe(0, 1), new Keyframe(20, 0.8f), new Keyframe(100, 0.3f));

    Rigidbody rb;

    private float ForwardVelocity
    {
        //change the velocity from local space to world space
        get { return transform.InverseTransformDirection(rb.velocity).z; }
    }

	// Use this for initialization
	void Awake ()
    {
        rb = GetComponent<Rigidbody>();
	}

    void FixedUpdate()
    {
        UpdateSteering();
        UpdateMotorTorque();
        UpdateBreakTorque();

        UpdateWheelModels();
    }

    private void UpdateWheelModels()
    {
        Vector3 positionToSet;
        Quaternion rotationToSet;

        for (int i = 0; i < allWheelModels.Length && i < allWheelColliders.Length; i++)
        {
            allWheelColliders[i].GetWorldPose(out positionToSet, out rotationToSet);
            allWheelModels[i].position = positionToSet;
            allWheelModels[i].rotation = rotationToSet;

        }
    }

    // Update is called once per frame
    void Update ()
    {
        GetInput();
	}

    void GetInput()
    {
        driveInput = Input.GetAxis("Vertical");
        steeringInput = Input.GetAxis("Horizontal");

        //Vector3 tankMovement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    }

    void UpdateSteering()
    {
        for (int x = 0; x < wheelsUsedForSteering.Length; x++)
        {
            wheelsUsedForSteering[x].steerAngle = maxSteeringAngle * steeringInput;
        }
    }

    void UpdateMotorTorque()
    {
        for (int x = 0; x < wheelsUsedForDriving.Length; x++)
        {
            wheelsUsedForDriving[x].motorTorque = maxMotorTorque * driveInput * torqueCurveModifier.Evaluate(rb.velocity.magnitude);
        }

        //Debug.Log("Potential Velocity: " + torqueCurveModifier.Evaluate(rb.velocity.magnitude));
        CapSpeed();

    }

    private void CapSpeed()
    {
        const float MPH = 2.23694f;

        float speedInMPH = rb.velocity.magnitude * MPH;
        if(speedInMPH > maxSpeedInMPH)
        {
            rb.velocity = (maxSpeedInMPH / MPH) * rb.velocity.normalized;
        }

        Debug.Log(speedInMPH);
    }

    void UpdateBreakTorque()
    {
        //if the forward velocity and the driveInput are both greater than zero (if true == true, this equals true)
        //if the forward velocity and the driveInput are both less than zero (if false == false, this equals true)
        //if the forward velocity and the driveInput are different (if true == false, this equals false)
        bool carIsMovingSameDirectionAsInput = ((ForwardVelocity > 0) == (driveInput > 0));

        //for (int x = 0; x < allWheelColliders.Length; x++)
        //{
        //    if (carIsMovingSameDirectionAsInput)
        //        wheelsUsedForDriving[x].motorTorque = maxMotorTorque * driveInput;
        //    else
        //        wheelsUsedForDriving[x].motorTorque = maxMotorTorque * driveInput * brakeSpeedMultiplier;
        //}

        float brakeTorqueToApply = 0f;
        if (!carIsMovingSameDirectionAsInput && driveInput != 0)
        {
            brakeTorqueToApply = brakeTorque;
        }

        for (int x = 0; x < allWheelColliders.Length; x++)
        {
            allWheelColliders[x].brakeTorque = brakeTorqueToApply;
        }
    }
}
