using UnityEngine;
using System.Collections;

public class TankMovement : MonoBehaviour
{
    [SerializeField] float maxSteeringAngle = 30f;
    [SerializeField] float maxMotorTorque = 400f;
    [SerializeField] float brakeSpeedMultiplier = 10f;
    [SerializeField] WheelCollider[] wheelsUsedForSteering, wheelsUsedForDriving;
    float driveInput, steeringInput;

    Rigidbody rb;

	// Use this for initialization
	void Awake ()
    {
        rb = GetComponent<Rigidbody>();
	}

    void FixedUpdate()
    {
        for (int x = 0; x < wheelsUsedForSteering.Length; x++)
        {
            wheelsUsedForSteering[x].steerAngle = maxSteeringAngle * steeringInput;
        }

        //brakes?
        float forwardVelocity = transform.InverseTransformDirection(rb.velocity).z; //change the velocity from local space to world space

        for (int x = 0; x < wheelsUsedForDriving.Length; x++)
        {
            if((forwardVelocity > 0 && driveInput > 0) || (forwardVelocity < 0 && driveInput < 0))
                wheelsUsedForDriving[x].motorTorque = maxMotorTorque * driveInput;
            else
                wheelsUsedForDriving[x].motorTorque = maxMotorTorque * driveInput * brakeSpeedMultiplier;
        }


    }

    // Update is called once per frame
    void Update ()
    {
        driveInput = Input.GetAxis("Vertical");
        steeringInput = Input.GetAxis("Horizontal");

        //Vector3 tankMovement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
	}
}
