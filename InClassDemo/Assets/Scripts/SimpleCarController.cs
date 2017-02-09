using UnityEngine;
using System.Collections;

public class SimpleCarController : MonoBehaviour
{
    [SerializeField]
    float maxSteerAngle = 30, maxMotorTorque = 500, brakeTorque = 50, maxSpeedInMPH = 7;

    [SerializeField]
    AnimationCurve torqueCurveModifier = new AnimationCurve(new Keyframe(0, 1), new Keyframe(100, 0.25f));

    [SerializeField]
    WheelCollider[] wheelsUsedForSteering, wheelsUsedForDriving, allWheelColliders;

    [SerializeField]
    Transform[] allWheelModels;

    Rigidbody rigidBody;

    float steeringInput, driveInput, brakeTorqueToApply;
    bool forwardVelocityIsSameAsInput;

    float ForwardVelocity { get { return transform.InverseTransformDirection(rigidBody.velocity).z; } }

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
        UpdateSteering();
        UpdateMotorTorque();
        CapSpeed();
        Brake();
        UpdateWheelModels();
    }

    void GetInput()
    {
        steeringInput = Input.GetAxis("Horizontal");
        driveInput = Input.GetAxis("Vertical");
    }

    void UpdateSteering()
    {
        for (int i = 0; i < wheelsUsedForSteering.Length; i++)
            wheelsUsedForSteering[i].steerAngle = steeringInput * maxSteerAngle;
    }

    void UpdateMotorTorque()
    {
        float curveMod = torqueCurveModifier.Evaluate(rigidBody.velocity.magnitude);

        print(curveMod);

        for (int i = 0; i < wheelsUsedForDriving.Length; i++)
            wheelsUsedForDriving[i].motorTorque = driveInput * maxMotorTorque * curveMod;
    }

    void CapSpeed()
    {
        const float mphConversion = 2.23694f;
        float currentSpeedInMph = rigidBody.velocity.magnitude * mphConversion;

        if (currentSpeedInMph > maxSpeedInMPH) rigidBody.velocity = (maxSpeedInMPH / mphConversion) * rigidBody.velocity.normalized;

        print(currentSpeedInMph);
    }

    void Brake()
    {
        brakeTorqueToApply = 0;
        forwardVelocityIsSameAsInput = (ForwardVelocity > 0) == (driveInput > 0);

        if (!forwardVelocityIsSameAsInput && driveInput != 0) brakeTorqueToApply = brakeTorque;

        for (int i = 0; i < allWheelColliders.Length; i++)
            allWheelColliders[i].brakeTorque = brakeTorqueToApply;
    }

    void UpdateWheelModels()
    {
        for (int i = 0; i < allWheelModels.Length; i++)
        {
            Vector3 positionToSet;
            Quaternion rotationToSet;

            allWheelColliders[i].GetWorldPose(out positionToSet, out rotationToSet);

            allWheelModels[i].position = positionToSet;
            allWheelModels[i].rotation = rotationToSet;
        }
    }
}
