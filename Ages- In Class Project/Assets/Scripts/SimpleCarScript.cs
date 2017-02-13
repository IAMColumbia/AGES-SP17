using UnityEngine;
using System.Collections;
using System;

public class SimpleCarScript : MonoBehaviour
{
    #region Editor Variables
    [SerializeField]
    float maxSteeringAngle = 30;

    [SerializeField]
    float maxMotorTorque = 400;

    [SerializeField]
    float breakTorque = 400;

    [SerializeField]
    WheelCollider[] wheelsUsedForSteering;

    [SerializeField]
    WheelCollider[] wheelsUsedForDriving;

    [SerializeField]
    WheelCollider[] allWheelColliders;

    [SerializeField]
    Transform[] allWheelModels;

    [SerializeField]
    AnimationCurve torqueCurveModifier =
        new AnimationCurve(new Keyframe(0, 1), new Keyframe(20, 0.8f), new Keyframe(100, 0.3f));

    [SerializeField]
    float maxSpeedInMPH = 10;
    #endregion
    #region Private Variables
    float driveInput;
    float steeringInput;
    Rigidbody rigidBody;
    float ForwardVelocity
    {
        //THIS IS HOW YOU USE PROPERTIES 
        //This is equal to "float forwardVelocity =transform.InverseTransformDirection(rigidBody.velocity).z;".
        get
        {
            return transform.InverseTransformDirection(rigidBody.velocity).z;
        }
    }
    #endregion

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
        UpdateSteering();
        UpdateMotorTorque();
        BreaksControl();
        UpdateWheelModels();

    }

    void UpdateWheelModels()
    {
        Vector3 positionToSet;
        Quaternion rotationToSet;
        for (int i = 0; i < allWheelModels.Length; i++)
        {
            
            //Is getting the position and rotation of the coliders and assigning them to variables.
            allWheelColliders[i].GetWorldPose(out positionToSet, out rotationToSet);
            allWheelModels[i].position = positionToSet;
            allWheelModels[i].rotation = rotationToSet;
        }
    }

    void UpdateMotorTorque()
    {
        for (int i = 0; i < wheelsUsedForDriving.Length; i++)
        {
            wheelsUsedForDriving[i].motorTorque = maxMotorTorque * driveInput
                * torqueCurveModifier.Evaluate(rigidBody.velocity.magnitude);

        }

        CapSpeed();

    }

    private void CapSpeed()
    {
        const float milesPerHourConst = 2.23694f;
        float speedInMPH = rigidBody.velocity.magnitude * milesPerHourConst;

        if (speedInMPH> maxSpeedInMPH)
        {
            rigidBody.velocity = maxSpeedInMPH / milesPerHourConst * rigidBody.velocity.normalized;
                
        }
    }

    void UpdateSteering()
    {
        for (int i = 0; i < wheelsUsedForSteering.Length; i++)
        {
            wheelsUsedForSteering[i].steerAngle = maxSteeringAngle * steeringInput;
        }
    }

    void GetInput()
    {
        driveInput = Input.GetAxis("Vertical");
        steeringInput = Input.GetAxis("Horizontal");
    }

    void BreaksControl()
    {
        //Breaks?
        //When forward Velocity is on direction and input velocity is in the opposite direction.
        

        bool carIsMovingSameDirectionAsInput = (ForwardVelocity > 0) == (driveInput > 0);

        float breakTorqueToApply = 0;
        if (!carIsMovingSameDirectionAsInput && driveInput !=0)
        {
            breakTorqueToApply = breakTorque;
        }
        for (int i = 0; i < allWheelColliders.Length; i++)
        {
            allWheelColliders[i].brakeTorque = breakTorqueToApply;
        }
    }
}
