using UnityEngine;
using System.Collections;
using System;

public class TankControllerV2 : MonoBehaviour 
{
    [SerializeField]
    private float maxMotorTorqueAcrossAllWheels = 120000;

    [SerializeField]
    private WheelCollider[] leftTrackWheelColliders;
    [SerializeField]
    private WheelCollider[] rightTrackWheelColliders;

    [SerializeField]
    float turnFriction = 0.3f;

    [SerializeField]
    float brakeTorque = 500;

    private float leftTrackInput;
    private float rightTrackInput;

    private WheelFrictionCurve normalFrictionCurve;
    private WheelFrictionCurve turnFrictionCurve;

    private Rigidbody rigidbody_useThis;

    private float ForwardVelocity
    {
        get
        {
            return transform.InverseTransformDirection(rigidbody_useThis.velocity).z; 
        }
    }

    private bool InputsAreSameDirection
    {
        get
        {
            return (leftTrackInput > 0) ==  (rightTrackInput > 0);
        }
    }

    private bool InputsAreOppositeOfForwardVelocity
    {
        get
        {
            return InputsAreSameDirection && !((ForwardVelocity > 0) == (leftTrackInput > 0));
        }
    }

    private bool InputsAreNeutral
    {
        get
        {
            return (leftTrackInput == 0) && (rightTrackInput == 0);
        }
    }

	// Use this for initialization
	void Start () 
	{
        normalFrictionCurve = leftTrackWheelColliders[0].sidewaysFriction;

        turnFrictionCurve = normalFrictionCurve;
        turnFrictionCurve.stiffness = turnFriction;

        rigidbody_useThis = GetComponent<Rigidbody>();

        Debug.Log("Normal Friction: " + normalFrictionCurve.stiffness); 

        Debug.Log("Turn Friction: " + turnFrictionCurve.stiffness);
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        GetInput();
    }

    private void GetInput()
    {
        leftTrackInput = Input.GetAxis("Left Tank Track");
        rightTrackInput = Input.GetAxis("Right Tank Track");
    }

    private void FixedUpdate()
    {
        ApplyMotorTorqueToTrackWheels(leftTrackWheelColliders, leftTrackInput);
        ApplyMotorTorqueToTrackWheels(rightTrackWheelColliders, rightTrackInput);

        TurnAssist();
        UpdateBrakeTorque(leftTrackWheelColliders);
        UpdateBrakeTorque(rightTrackWheelColliders);
    }

    private void UpdateBrakeTorque(WheelCollider[] track)
    {
        float brakeTorqueToApply = 0;

        Debug.Log("Inputs Are opposite of velocity? " + InputsAreOppositeOfForwardVelocity);
        if (InputsAreNeutral || InputsAreOppositeOfForwardVelocity)
        {
            brakeTorqueToApply = brakeTorque;
        }

        for (int i = 0; i < track.Length; i++)
        {
            track[i].brakeTorque = brakeTorqueToApply;
        }
    }

    private void TurnAssist()
    {
        UpdateSidewaysFriction(leftTrackWheelColliders);
        UpdateSidewaysFriction(rightTrackWheelColliders);
    }

    private void UpdateSidewaysFriction(WheelCollider[] track)
    {
        if (!InputsAreNeutral && !InputsAreSameDirection)
        {
            for (int i = 0; i < track.Length; i++)
            {
                track[i].sidewaysFriction = turnFrictionCurve;
            }
        }
        else
        {
            for (int i = 0; i < track.Length; i++)
            {
                track[i].sidewaysFriction = normalFrictionCurve;
            }
        }
    }

    private void ApplyMotorTorqueToTrackWheels(WheelCollider[] track, float input)
    {
        float motorTorqueToApply = maxMotorTorqueAcrossAllWheels / track.Length * input;

        for (int i = 0; i < track.Length; i++)
        {
            track[i].motorTorque = motorTorqueToApply;
        }
    }
}
