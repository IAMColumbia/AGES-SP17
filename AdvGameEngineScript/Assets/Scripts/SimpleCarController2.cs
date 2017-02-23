using UnityEngine;
using System.Collections;
using System;

public class SimpleCarController2 : MonoBehaviour
{

    // Use this for initialization


    [SerializeField]
    float maxSteerAngle = 30;

    [SerializeField]
    float motorTorque = 500;
    [SerializeField]
    float brakeTorque = 50;


    [SerializeField]
    WheelCollider[] wheelsUsedForSteering; //Ask why he'd call it that and no wheelColliders

    [SerializeField]
    WheelCollider[] wheelsUsedForDriving;

    [SerializeField]
    AnimationCurve torqueCurveModifier = new AnimationCurve(new Keyframe(0,1), new Keyframe(100, 0.25f));

    [SerializeField]
    WheelCollider[] allWheelColliders;

    [SerializeField]
    Transform[] allWheelModels;

    [SerializeField]
    float maxSpeedInMPH = 25;

    float steeringInput;
    float driveInput;
    

    float ForwardVelocity
    { 
        get
        {
            return transform.InverseTransformDirection(rigidBody.velocity).z;
        }
     }
Rigidbody rigidBody;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }
    void Update()
    {
        getInput();
    }
    void FixedUpdate()
    {
        UpdateSteering();

        UpdateMotorTorque();

        UpdateWheelModels();
        CapSpeed();
      // float forwardVelocity = transform.InverseTransformDirection(rigidBody.velocity).z;

        float brakeTorqueToApply = 0;

        bool forwardVelocityIsSameAsInput = (ForwardVelocity > 0) == (driveInput > 0);

        if (!forwardVelocityIsSameAsInput && driveInput != 0)
        {
            brakeTorqueToApply = brakeTorque;
        }

        for (int i = 0; i < allWheelColliders.Length; i++)
        {
            allWheelColliders[i].brakeTorque = brakeTorqueToApply;
         
            //TODO if forwardVelocit matches inut, then add motortorque
            //if forwardVelocity is opposite of input, add brakeTorque
        }
    }

    private void CapSpeed()
    {
        const float conversionMPH = 2.23693699f;

        float currentSpeedMPH = rigidBody.velocity.magnitude * conversionMPH;

        Debug.Log("Current Speed: " + currentSpeedMPH);
        if (currentSpeedMPH > maxSpeedInMPH)
        {
            rigidBody.velocity = (maxSpeedInMPH / conversionMPH) * rigidBody.velocity.normalized;
        }
        throw new NotImplementedException();
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

    private void UpdateMotorTorque()
    {
        float curveMod = torqueCurveModifier.Evaluate(rigidBody.velocity.magnitude);

        Debug.Log("Curve Log: " + curveMod);
        for (int i = 0; i < wheelsUsedForDriving.Length; i++)
        {
            wheelsUsedForDriving[i].motorTorque = driveInput * motorTorque * curveMod;
        }
    }

    private void UpdateSteering()
    {
        for (int i = 0; i < wheelsUsedForSteering.Length; i++)
        {
            wheelsUsedForSteering[i].steerAngle = steeringInput * maxSteerAngle;
        }
    }

    void getInput()
    {
        steeringInput = Input.GetAxis("Horizontal");
        driveInput = Input.GetAxis("Vertical");
        // float yInput = Input.GetAxis("Vertical"); We wouldn't use his for driving I guess
    }

    // Update is called once per frame

}
