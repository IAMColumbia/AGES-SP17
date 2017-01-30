using UnityEngine;
using System.Collections;

public class SimpleCarController : MonoBehaviour
{
    [SerializeField] private float maxSteeringAngle = 30;
    [SerializeField] private float maxMotorTorque = 400;
    [SerializeField] private WheelCollider[] wheelsUsedForSteering;
    [SerializeField] private WheelCollider[] wheelsUsedForDriving;

    private float driveInput;
    private float steeringInput;
    private Rigidbody myRigidbody;


    void Awake ()
    {
        myRigidbody = GetComponent<Rigidbody>();
	}
	
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

        float forwardVelocity = transform.InverseTransformDirection(myRigidbody.velocity).z;
    }

}
