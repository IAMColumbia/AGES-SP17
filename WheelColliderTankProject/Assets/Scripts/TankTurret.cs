using UnityEngine;
using System.Collections;
using System;

public class TankTurret : MonoBehaviour 
{
    // This class controls the tank turret rotation.
    // That's pretty much it.

    // The turret is a rigid body so it isn't so solid it stops another tank in its tracks.

    // In order to have the turret not swivel around when the tank
    // is moving and turning, we use a configurable joint and
    // set its target rotation to "lock" it in a somewhat "gentle" way.
    // This way things that bump it still move it a little.

    [SerializeField]
    private float rotationSpeed = 50;

    private float rotationInput;

    private Rigidbody rigidbody_use;

    private ConfigurableJoint joint;

    // The initial settings on the jointDrive are used for
    // the "locked" settings.
    private Quaternion lockedRotation;
    private JointDrive lockedJointDrive;

    private JointDrive unlockedJointDrive;

    private bool resetRotationPressed = false;

	// Use this for initialization
	void Start () 
	{
        rigidbody_use = GetComponent<Rigidbody>();
        joint = GetComponent<ConfigurableJoint>();

        lockedRotation = transform.localRotation;
        lockedJointDrive = joint.slerpDrive;

        unlockedJointDrive = lockedJointDrive;
        unlockedJointDrive.positionSpring = 0;
    }
	
	// Update is called once per frame
	void Update () 
	{
        GetRotationInput();
	}

    private void GetRotationInput()
    {
        rotationInput = Input.GetAxis("RotateTurret");

        resetRotationPressed = Input.GetButtonDown("ResetTurret");
    }

    private void FixedUpdate()
    {
        // We use the "locked" joint drive with a heavy duty spring to
        // lock the rotation of the turret when we aren't trying to rotate it.
        if (rotationInput == 0)
        {
            joint.slerpDrive = lockedJointDrive;
        }
        else
        {
            joint.slerpDrive = unlockedJointDrive;

            float rotationAmount = -1 * rotationInput * rotationSpeed * Time.deltaTime;

            Quaternion newRotation = Quaternion.Euler(0, rotationAmount, 0) * rigidbody_use.rotation;

            rigidbody_use.MoveRotation(newRotation);

            // Update the joint's target rotation so it "locks" to that rotation now.
            // Not sure why it needs to be the opposite of our local rotation, but it does...
            joint.targetRotation = Quaternion.Inverse(lockedRotation);
        }

        HandleResetPressed();
    }

    private void HandleResetPressed()
    {
        // Resetting the rotation helps players figure out which way the front of the tank is.
        if (resetRotationPressed)
        {
            joint.targetRotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
