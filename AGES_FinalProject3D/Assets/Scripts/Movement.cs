using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    [SerializeField]
    private string leftStickVertical;
    [SerializeField]
    private string leftStickHorizontal;
    [SerializeField]
    private string slowDownButton;
    [SerializeField]
    private float turnMultiplier;
    [SerializeField]
    private float turnMultiplierWhenSlow;
    [SerializeField]
    private float accelerationSpeed;
    [SerializeField]
    private float slowdownSpeed;
    [SerializeField]
    private float minSpeedinMPH;
    [SerializeField]
    private float maxSpeedinMPH;

    private Rigidbody playerRigidBody;

    private float leftStickInputVertical;
    private float leftStickInputHorizontal;
    private float inGameTurnMultiplier;

    private float currentVerticalTilt;
    private float currentHorizontalTilt;

    private const float zeroConstant = 0;
    private const float rotationEulerConstant = 360;

	// Use this for initialization
	void Start ()
    {
        playerRigidBody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        UpdateTurnInput();
        UpdateCurrentTilt();
        Debug.Log("acceleration speed is " + accelerationSpeed.ToString());
    }


    private void FixedUpdate()
    {
        AccelerateForward();
        MoveRotation();
    }


    private void UpdateTurnInput()
    {
        leftStickInputVertical = (Input.GetAxis(leftStickVertical) * inGameTurnMultiplier);
        leftStickInputHorizontal = (Input.GetAxis(leftStickHorizontal) * inGameTurnMultiplier);
    }

    private void UpdateCurrentTilt()
    {
        currentVerticalTilt = gameObject.transform.rotation.eulerAngles.x;

        currentHorizontalTilt = gameObject.transform.rotation.eulerAngles.y;

        //Debug.Log("X tilt is " + currentVerticalTilt + " Y tilt is " + currentHorizontalTilt);
    }

    private void AccelerateForward()
    {
        playerRigidBody.AddRelativeForce(zeroConstant, zeroConstant, accelerationSpeed,ForceMode.Force);

        //Add condition so you can't slow down too much
        if (Input.GetButton(slowDownButton))
        {
            SlowDown();
            inGameTurnMultiplier = Mathf.Lerp(turnMultiplierWhenSlow, turnMultiplier, Time.deltaTime);
        }
        else
        {
            inGameTurnMultiplier = Mathf.Lerp(turnMultiplier, turnMultiplierWhenSlow,Time.deltaTime);
        }
    }

    private void SlowDown()
    {
        accelerationSpeed = slowdownSpeed;
    }

    private void MoveRotation()
    {
        //gameObject.transform.Translate(leftStickInputHorizontal, leftStickInputVertical, zeroConstant, Space.World);

        Vector3 positionToMoveTo = new Vector3(leftStickInputHorizontal, leftStickInputVertical);

        Quaternion roatationToRotateTo = Quaternion.Euler(leftStickInputVertical, leftStickInputHorizontal, zeroConstant);

        playerRigidBody.MoveRotation(roatationToRotateTo);

        //playerRigidBody.MovePosition(positionToMoveTo);

        //Debug.Log("X input is " + leftStickInputVertical + " Y input is " + leftStickInputHorizontal);
    }
}
