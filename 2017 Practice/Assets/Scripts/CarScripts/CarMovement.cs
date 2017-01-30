using UnityEngine;
using System.Collections;

public class CarMovement : MonoBehaviour {
    [SerializeField]
    private float maxSteeringAngle = 30;
    [SerializeField]
    private float maxMotorTorque = 300;


    Rigidbody rb;

    [SerializeField]
    private WheelCollider[] wheelsUsedForSteering;
    [SerializeField]
    private WheelCollider[] wheelsUsedForDriving;

    float driveInput;
    float steeringInput;

    [SerializeField]
    float carSpeed;

	// Use this for initialization
	void Awake ()
    {
        rb = GetComponent<Rigidbody>();
	
	}

    // Update is called once per frame
    void Update()
    {

        driveInput = Input.GetAxis("Vertical_P1");
        steeringInput = Input.GetAxis("Horizontal_P1");

  


    }

   void FixedUpdate ()
    {

        for (int i = 0; i < wheelsUsedForSteering.Length; i++)
        {

            wheelsUsedForSteering[i].steerAngle = maxSteeringAngle * steeringInput;
        }

        for (int i = 0; i < wheelsUsedForDriving.Length; i++)
        {
            wheelsUsedForDriving[i].motorTorque = maxMotorTorque * driveInput;
        }

        // Brakes?
        float forwarVelocity = transform.InverseTransformDirection(rb.velocity).z;
       

    }




}
