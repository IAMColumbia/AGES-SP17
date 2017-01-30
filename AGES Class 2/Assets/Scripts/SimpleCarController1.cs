using UnityEngine;
using System.Collections;

public class SimpleCarController1 : MonoBehaviour {
    [SerializeField]
    private float maxSteeringAngle = 30;
    [SerializeField]
    private float maxMotorTorque = 400;
    [SerializeField]
    private WheelCollider[] wheelsUsedForSteering;
    [SerializeField]
    private WheelCollider[] wheelsUsedforDriving;

    private float driveInput;
    private float steeringInput;
    private Rigidbody rigidBody;
    void Awake () {

        rigidBody = GetComponent<Rigidbody>();
	}
	
	void Update () {

       driveInput = Input.GetAxis("Vertical");
       steeringInput = Input.GetAxis("Horizontal");
	}

    void FixedUpdate()
    {
        for (int i = 0; i < wheelsUsedForSteering.Length; i++)
        {
            wheelsUsedForSteering[i].steerAngle = maxSteeringAngle * steeringInput;
        }

        for (int i = 0; i < wheelsUsedforDriving.Length; i++)
        {
            wheelsUsedforDriving[i].motorTorque = maxMotorTorque * driveInput;
        }

        float forwardVelocity = transform.InverseTransformDirection(rigidBody.velocity).z;
    }
}
