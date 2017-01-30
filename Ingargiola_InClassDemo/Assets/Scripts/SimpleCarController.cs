using UnityEngine;
using System.Collections;

public class SimpleCarController : MonoBehaviour
{
    [SerializeField] private float maxSteeringAngle = 30;
    [SerializeField] private float maxMotorTorque =400;
    
    [SerializeField] private WheelCollider[] wheelsUsedForSteering;
    [SerializeField] private WheelCollider[] wheelsUsedForDriving;

    private float driveInput;
    private float steeringInput;
    private Rigidbody rigidBody;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame, need * Time.deltaTime;
    void Update ()
    {
       driveInput = Input.GetAxis("Vertical");
       steeringInput = Input.GetAxis("Horizontal");

       
	}

    //called every fixed interval of time, don't need * Time.deltaTime;
    private void FixedUpdate()
    {
        //for loop for an array
        for (int i = 0; i < wheelsUsedForSteering.Length; i++)
        {
            wheelsUsedForSteering[i].steerAngle = maxSteeringAngle * steeringInput;
        }

        for (int i = 0; i < wheelsUsedForDriving.Length; i++)
        {
            wheelsUsedForDriving[i].motorTorque = maxMotorTorque * driveInput;
        }

        //brakes?
        rigidBody.velocity
    }
}
