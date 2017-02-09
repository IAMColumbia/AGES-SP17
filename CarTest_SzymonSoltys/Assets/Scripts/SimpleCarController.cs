using System;
using UnityEngine;
//using System.Collections;
//using System;

public class SimpleCarController : MonoBehaviour {

    [SerializeField]
    private float maxSteerAngle = 30;

    [SerializeField]
    private AnimationCurve torqueCurveModifier = new AnimationCurve(new Keyframe(0, 1), new Keyframe(100,0.25f));

    [SerializeField]
    private float maxMotorTorque = 500;

    [SerializeField]
    private float maxBrakeTorque;

    [SerializeField]
    private float maxSpeed = 7;

    [SerializeField]
    private WheelCollider[]  wheelsUsedForSteering;

    [SerializeField]
    private WheelCollider[] wheelsUsedForDriving;

    [SerializeField]
    private WheelCollider[] wheelsUsedForBraking;

    [SerializeField]
    private WheelCollider[] allWheelColliders;

    [SerializeField]
    private Transform[] allWheelModels;


    private float steeringinput;
    private float drivinginput;
    //private float brakinginput;

    private Rigidbody bodyThatIsRigid;
    private float ForwardVelocity
    {
        get
        {
            return transform.InverseTransformDirection(bodyThatIsRigid.velocity).z;
        }

    }

    // Use this for initialization
    void Start ()
    {
        bodyThatIsRigid = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
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

    private void CapSpeed()
    {
        const float mphConversion = 2.23693629f;

        float currentSpeedInMPH = GetComponent<Rigidbody>().velocity.magnitude * mphConversion;

        if(currentSpeedInMPH > maxSpeed)
        {
            GetComponent<Rigidbody>().velocity = (currentSpeedInMPH / mphConversion) * GetComponent<Rigidbody>().velocity.normalized;
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

        //if forward velocity doesnt match input, then add brake torque

        float brakeTorqueToApply = 0;

        bool forwardVelocityIsSameAsInput = (ForwardVelocity > 0) == (drivinginput > 0);

        if (!forwardVelocityIsSameAsInput && drivinginput != 0)
        {
            brakeTorqueToApply = maxBrakeTorque;
        }

        for (int i = 0; i < wheelsUsedForBraking.Length; i++)
        {
            wheelsUsedForBraking[i].brakeTorque = brakeTorqueToApply;        
        }
    }

    private void UpdateSteering()
    {
        for (int i = 0; i < wheelsUsedForSteering.Length; i++)
        {
            wheelsUsedForSteering[i].steerAngle = steeringinput * maxSteerAngle;
        }
    }

    private void UpdateMotorTorque()
    {
        
        float curveMod = torqueCurveModifier.Evaluate(GetComponent<Rigidbody>().velocity.magnitude);

        Debug.Log("Current Curve: " + curveMod);
        for (int i = 0; i < wheelsUsedForDriving.Length; i++)
        {
            wheelsUsedForDriving[i].motorTorque = drivinginput * maxMotorTorque;
        }
    }

    private void GetInput()
    {
        steeringinput = Input.GetAxis("Horizontal");
        drivinginput = Input.GetAxis("Vertical");
        //brakinginput = Input.GetAxis("Jump");
    }
}
