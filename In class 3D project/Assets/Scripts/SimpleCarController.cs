using UnityEngine;
using System.Collections;

public class SimpleCarController : MonoBehaviour
{
    [SerializeField]
    float maxSteeringAngle = 30;

    [SerializeField]
    float maxMotorTorque = 400;

    [SerializeField]
    WheelCollider[] wheelsUsedForSteering;

    [SerializeField]
    WheelCollider[] wheelsUsedForDriving;

    float driveInput;
    float steeringInput;
    Rigidbody rigidBody;

    // Use this for initialization
    void Awake ()
    {
        rigidBody = GetComponent<Rigidbody>();
         
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        driveInput = Input.GetAxis("Vertical");
        steeringInput = Input.GetAxis("Horizontal");  
    }

    void FixedUpdate()
    {
        for (int i = 0; i < wheelsUsedForSteering.Length; i++)
        {
            wheelsUsedForSteering[i].steerAngle = maxSteeringAngle * steeringInput;
        }

        for (int i = 0; i < wheelsUsedForDriving.Length; i++)
        {
            wheelsUsedForDriving[i].motorTorque = maxMotorTorque * driveInput;
        }

        //Brakes?
        float forwardVelocity = transform.InverseTransformDirection(rigidBody.velocity).z;
    }
}
