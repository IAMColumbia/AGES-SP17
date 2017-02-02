using UnityEngine;
using System.Collections;
using System;

public class TankController : MonoBehaviour 
{
    [SerializeField]
    private float turnSpeed = 100;

    [SerializeField]
    private float maxForwardMotorTorque = 300;

    [SerializeField]
    private float maxSpeedMPH = 30;

    [SerializeField]
    AnimationCurve torqueCurveModifier1 = new AnimationCurve(new Keyframe(0, 1.0f), new Keyframe(20.0f, 0.8f), new Keyframe(100.0f, 0.1f));

    [SerializeField]
    private float brakeTorque = 100;


    [SerializeField]
    private float brakeDrag = 20;
    [SerializeField]
    private float brakeAnglularDrag = 1;

    [SerializeField]
    private WheelCollider[] leftTrackWheelColliders;

    [SerializeField]
    private WheelCollider[] rightTrackWheelColliders;

    private float leftTrackInput;
    private float rightTrackInput;
    private float brakeInput;

    private Rigidbody rigidBody;
    private float originalDrag;
    private float originalAngularDrag;

	// Use this for initialization
	void Start () 
	{
        rigidBody = GetComponent<Rigidbody>();
        originalAngularDrag = rigidBody.angularDrag;
        originalDrag = rigidBody.drag;
	}
	
	// Update is called once per frame
	private void Update () 
	{
        GetInput();
	}

    private void FixedUpdate()
    {
        Drive(leftTrackInput, rightTrackInput);
        CapSpeed();
    }

    private void Drive(float leftTrackInput, float rightTrackInput)
    {
        for (int i = 0; i < leftTrackWheelColliders.Length; i++)
        {
            leftTrackWheelColliders[i].motorTorque = maxForwardMotorTorque * leftTrackInput;// * torqueCurveModifier1.Evaluate(rigidBody.velocity.magnitude);
        }

        for (int i = 0; i < rightTrackWheelColliders.Length; i++)
        {
            rightTrackWheelColliders[i].motorTorque = maxForwardMotorTorque * rightTrackInput;// * torqueCurveModifier1.Evaluate(rigidBody.velocity.magnitude);
        }


        bool sameInputDirection = (leftTrackInput > 0) == (rightTrackInput > 0);
        float forwardVelocity = transform.InverseTransformDirection(rigidBody.velocity).z;
        bool tryingToMoveOppositeOfForwardVelocity = sameInputDirection && !((leftTrackInput > 0) == (forwardVelocity > 0));

        float threshold = 1;
        if ((leftTrackInput == 0 && rightTrackInput == 0) || (tryingToMoveOppositeOfForwardVelocity && Mathf.Abs(forwardVelocity) > threshold))
        {
            rigidBody.drag = brakeDrag;
            rigidBody.angularDrag = brakeAnglularDrag;
            for (int i = 0; i < leftTrackWheelColliders.Length; i++)
            {
                leftTrackWheelColliders[i].brakeTorque = brakeTorque;
                rightTrackWheelColliders[i].brakeTorque = brakeTorque;
            }
        }
        else
        {
            rigidBody.drag = originalDrag;
            rigidBody.angularDrag = originalAngularDrag;
            for (int i = 0; i < leftTrackWheelColliders.Length; i++)
            {
                leftTrackWheelColliders[i].brakeTorque = 0;
                rightTrackWheelColliders[i].brakeTorque = 0;
            }
        }

        if (!sameInputDirection)
        {
            rigidBody.drag = brakeDrag;
        }
        if (sameInputDirection)
        {
            rigidBody.angularDrag = brakeAnglularDrag;
        }


        // Conditions to apply brake (brake stops the wheels from rolling)
        // - Trying to move opposite of forward velocity
        //      Because we we want the tank to stop responsively when the player tries to switch directions
        // - Joysticks are in neutral position
        //      Because we don't want the tank rolling when it's trying to stop

        // Conditions to apply drag (drag stops the tank rigid body from sliding)
        // - Trying to move opposite of forward velocity
        //      Because we we want the tank to stop responsively when the player tries to switch directions
        // - When the player tires to turn (aka joysticks are pushed in opposite directions)
        //      Because we want the tank to be able to turn without sliding

        // Conditions to apply angular drag (angular drag stops the tank rigid body from spinning)
        // - joysticks are in neutral position
        //      Because we don't want the tank spinning when it's trying to stop
        // - joysticks are pushed in same direction
        //      Because we don't want the tank spinning when it's trying to move straight

    }

    private void CapSpeed()
    {
        float speed = rigidBody.velocity.magnitude;
        speed *= 2.23693629f;
        if (speed > maxSpeedMPH)
            rigidBody.velocity = (maxSpeedMPH / 2.23693629f) * rigidBody.velocity.normalized;
    }

    private void GetInput()
    {
        leftTrackInput = Input.GetAxis("Left Tank Track");
        rightTrackInput = Input.GetAxis("Right Tank Track");
    }
}
