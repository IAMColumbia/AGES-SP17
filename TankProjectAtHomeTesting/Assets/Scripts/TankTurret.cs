using UnityEngine;
using System.Collections;
using System;

public class TankTurret : MonoBehaviour 
{
    [SerializeField]
    private float rotationSpeed = 50;

    private float input;
    private Rigidbody rigidbody_use;

    private ConfigurableJoint joint;

    private Quaternion lockedRotation;
    private JointDrive lockedJointDrive;
    private JointDrive unlockedJointDrive;

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
        GetInput();
	}

    private void GetInput()
    {
        input = Input.GetAxis("RotateTurret");
    }

    private void FixedUpdate()
    {
        if (input == 0)
        {
            joint.slerpDrive = lockedJointDrive;
        }
        else
        {
            joint.slerpDrive = unlockedJointDrive;
            
            float rotationAmount = -1 * input * rotationSpeed * Time.deltaTime;

            Quaternion newRotation = Quaternion.Euler(0, rotationAmount, 0) * rigidbody_use.rotation;

            rigidbody_use.MoveRotation(newRotation);

            // Not sure why it needs to be the opposite of my local rotation, but it does...
            joint.targetRotation = Quaternion.Inverse(transform.localRotation);            
            
            // TODO: add a snap to front / default orientation button? Limit the max speed it rotates back? 
        }
    }
}
