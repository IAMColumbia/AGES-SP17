using UnityEngine;
using System.Collections;
using System;

public class TankController : MonoBehaviour, IHeavyExplodableObject
{
    // This is the main class for controlling the tank.
    // It handles driving and steering.
    // It also handles IHeavyExplodableObject "exploding," but maybe it shouldn't...
    #region Variables
    [Tooltip("Total torque divided across all wheels.")]
    [SerializeField]
    private float maxMotorTorqueAcrossAllWheels = 120000;

    [SerializeField]
    private WheelCollider[] leftTrackWheelColliders;

    [SerializeField]
    private WheelCollider[] rightTrackWheelColliders;

    [Tooltip("Wheel friction is lowered when turning to improve responsiveness.")]
    [SerializeField]
    float turnFriction = 0.5f;

    [SerializeField]
    float brakeTorque = 500;

    [Tooltip("Angular drag is increased when we're trying to slow down the tank.")]
    [SerializeField]
    float slowAngularDrag = 1.5f;

    [Tooltip("Drag is increased when we're trying to slow down the tank.")]
    [SerializeField]
    float slowDrag = 1;

    [Tooltip("When hit by a shell, force is applied at this location to 'rock' the tank.")]
    [SerializeField]
    Transform explosionPoint;

    [Tooltip("When hit by a shell, we use this much force to 'rock' the tank.")]
    [SerializeField]
    float explosionForce = 7000000;

    private float leftTrackInput;
    private float rightTrackInput;

    private WheelFrictionCurve normalFrictionCurve;
    private WheelFrictionCurve turnFrictionCurve;

    private float normalDrag;
    private float normalAngularDrag;

    private Rigidbody rigidbody_useThis;

    private float ForwardVelocity
    {
        get
        {
            return transform.InverseTransformDirection(rigidbody_useThis.velocity).z; 
        }
    }

    // Returns true if left and right track input are the same direction
    private bool InputsAreSameDirection
    {
        get
        {
            return (leftTrackInput > 0) ==  (rightTrackInput > 0);
        }
    }

    // Returns true if left and right inputs are opposite of forward velocity
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
    #endregion
    // Use this for initialization
    void Start () 
	{
        rigidbody_useThis = GetComponent<Rigidbody>();

        normalFrictionCurve = leftTrackWheelColliders[0].sidewaysFriction;

        turnFrictionCurve = normalFrictionCurve;
        turnFrictionCurve.stiffness = turnFriction;

        normalAngularDrag = rigidbody_useThis.angularDrag;
        normalDrag = rigidbody_useThis.drag;
	}

    // Colors the tank for differentiating multiple players


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

        UpdateDrag();
    }

    // Apply brakes if inputs are neutral or 
    // we are trying to move opposite direction of our velocity.
    private void UpdateBrakeTorque(WheelCollider[] track)
    {
        float brakeTorqueToApply = 0;

        if (InputsAreNeutral || InputsAreOppositeOfForwardVelocity)
        {
            brakeTorqueToApply = brakeTorque;
        }

        for (int i = 0; i < track.Length; i++)
        {
            track[i].brakeTorque = brakeTorqueToApply;
        }
    }

    // Reduce friction when turning to improve responsiveness.
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

    // TODO: reduce wheel friction when tank is stopped, so other tanks that run into a stopped tank feels more satisfying

    private void UpdateDrag()
    {
        // Conditions to apply drag (drag stops the tank rigid body from sliding)
        // - Trying to move opposite of forward velocity
        //      Because we we want the tank to stop responsively when the player tries to switch directions
        // - When the player tires to turn (aka joysticks are pushed in opposite directions)
        //      Because we want the tank to be able to turn without sliding too much
        // - Inputs are neutral (player trying to stop)

        if (InputsAreNeutral || InputsAreOppositeOfForwardVelocity)
        {
            rigidbody_useThis.drag = slowDrag;
        }
        else
        {
            rigidbody_useThis.drag = normalDrag;
        }

        // Conditions to apply angular drag (angular drag stops the tank rigid body from spinning)
        // - joysticks are in neutral position
        //      Because we don't want the tank spinning when it's trying to stop
        // - joysticks are pushed in same direction
        //      Because we don't want the tank spinning when it's trying to move straight

        if (InputsAreNeutral || InputsAreSameDirection)
        {
            rigidbody_useThis.angularDrag = slowAngularDrag;
        }
        else
        {
            rigidbody_useThis.angularDrag = normalAngularDrag;
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

    // IHeavyExplodableObject implementation
    // We use this because the tanks are so massive that they need special
    // behavior to "explode" in a satisfying way when hit by shells, etc.
    public void Explode(Vector3 incomingProjectileDirection)
    {
        Vector3 explosionDirection = Vector3.up + incomingProjectileDirection;
        rigidbody_useThis.AddForceAtPosition(explosionForce * explosionDirection, explosionPoint.position);
    }
}
