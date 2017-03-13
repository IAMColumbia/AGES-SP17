using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class TankMovement : MonoBehaviour
{
    #region SerializedFields
    [SerializeField]
    private string triggers;
    [SerializeField]
    private string leftStick;
    [SerializeField]
    private string driftButton;
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private float turnSpeed;
    [SerializeField]
    private float driftTurnSpeed;
    [SerializeField]
    private float maxSpeedInMPH;
    #endregion

    private float triggerInput;
    private float leftStickInput;
    private bool isAccelerating;
    private Vector3 movementVector;
    private Rigidbody myRigidBody;

    #region Constants
    private const float yConstant = 0;
    private const float xConstant = 0;
    private const float zConstant = 0;
    #endregion

    // Use this for initialization
    void Start()
    {
        //gets the rigidbody component, which is used for acceleration and turning
        myRigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Turn();
        Accelerate();
    }

    private void Accelerate()
    {
        /*gets a value from the controller tiggers, then multiplies said value by negative one
        (this is because the left trigger returns a positive value, and the right trigger returns a negative value)
        */
        triggerInput = (Input.GetAxis(triggers) * -1);
        movementVector = new Vector3(xConstant, yConstant, triggerInput);

        //converts to MPH
        const float milesPerHourConst = 2.23694f;
        float speedInMPH = myRigidBody.velocity.magnitude * milesPerHourConst;

        //TODO: Check speed for Reversing if needs revision

        //if statement caps speed
        if (speedInMPH < maxSpeedInMPH)
        {
            //movement speed is actual "speed" of zamboni
            myRigidBody.AddRelativeForce(movementVector * movementSpeed);

            //debug statement to check if it's going faster than max speed
            //Debug.Log("Speed is at " + (myRigidBody.velocity.magnitude * milesPerHourConst).ToString());
        }

        UpdateIsAccelerating();

        //Debug.Log(triggerInput.ToString());
    }

    private void UpdateIsAccelerating()
    {
        if ((Input.GetAxis(triggers) > 0.1) || (Input.GetAxis(triggers) < -0.1))
        {
            isAccelerating = true;
        }
        else
        {
            isAccelerating = false;
        }
    }

    private void Turn()
    {
        if (isAccelerating)
        {
            if (Input.GetAxis(driftButton) >= 0.1)
            {
                leftStickInput = (Input.GetAxis(leftStick) * driftTurnSpeed);
                Debug.Log("I'm drifting!");
            }
            else
            {
                leftStickInput = (Input.GetAxis(leftStick) * turnSpeed);
            }
            
            gameObject.transform.Rotate(xConstant, leftStickInput, zConstant);
        }
        //Debug.Log(leftStickInput.ToString());
    }
}
