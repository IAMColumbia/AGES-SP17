using UnityEngine;
using System.Collections;

public class SimpleCarController : MonoBehaviour
{
    [SerializeField]
    private float maxSteeringAngle = 30;
    [SerializeField]
    private float maxMotorTorque = 400;
    [SerializeField]
    private WheelCollider[] wheelsUsedForSteering;
    [SerializeField]
    private WheelCollider[] wheelsUsedForDriving;

    private float driveInput;
    private float steeringInput;
    private Rigidbody rigidBody;

    // Use this for initialization
    private void Awake ()
    {
        rigidBody = GetComponent<Rigidbody>();
	}

    // Update is called once per frame
    private void Update ()
    {

        driveInput = Input.GetAxis("Vertical");
        steeringInput = Input.GetAxis("Horizontal");
        
    }

    private void FixedUpdate()
    {
        HandleSteering();

        HandleDriving();

        //Brakes?
        float forwardVelocity = transform.InverseTransformDirection(rigidBody.velocity).z;

    }

    private void HandleDriving()
    {
        for (int i = 0; i < wheelsUsedForDriving.Length; i++)
        {
            wheelsUsedForDriving[i].motorTorque = maxMotorTorque * driveInput;
        }
    }

    private void HandleSteering()
    {
        for (int i = 0; i < wheelsUsedForSteering.Length; i++)
        {
            wheelsUsedForSteering[i].steerAngle = maxSteeringAngle * steeringInput;
        }
    }
}
