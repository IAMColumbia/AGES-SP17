using UnityEngine;
using System.Collections;

public class SimpleCarController : MonoBehaviour
{
    [SerializeField]
    float maxSteerAngle = 30;

    [SerializeField]
    float maxMotorTorque = 500;

    [SerializeField]
    float brakeTorque = 50;

    [SerializeField]
    WheelCollider[] wheelsUsedForSteering;

    [SerializeField]
    WheelCollider[] wheelsUsedForDriving;

    [SerializeField]
    WheelCollider[] allWheelColliders;

    Rigidbody rigidBody;

    float steeringInput;
    float driveInput;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        GetInput();
    }

    void FixedUpdate()
    {

        for (int i = 0; i < wheelsUsedForSteering.Length; i++)
        {
            wheelsUsedForSteering[i].steerAngle = steeringInput * maxSteerAngle;
        }

        for (int i = 0; i < wheelsUsedForDriving.Length; i++)
        {
            wheelsUsedForDriving[i].motorTorque = driveInput * maxMotorTorque;
        }

        float forwardVelocity = transform.InverseTransformDirection(rigidBody.velocity).z;
        for (int i = 0; i < allWheelColliders.Length; i++)
        {
            // TODO implement braking
            // if forward velocity matches input, then add motor torque
            // if forward velocity is opposite of input, add brake torque
        }
    }

    void GetInput()
    {
        steeringInput = Input.GetAxis("Horizontal");
        driveInput = Input.GetAxis("Vertical");
    }
}
