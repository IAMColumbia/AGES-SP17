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
    private float turnSpeed;
    [SerializeField]
    private float maxVerticalTilt;
    [SerializeField]
    private float maxHorizontalTilt;
    [SerializeField]
    private float accelerationSpeed;
    [SerializeField]
    private float maxSpeedinMPH;

    private Rigidbody playerRigidBody;

    private float leftStickInputVertical;
    private float leftStickInputHorizontal;

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
    }


    private void FixedUpdate()
    {
        //AccelerateForward();
        Turn();
    }


    private void UpdateTurnInput()
    {
        leftStickInputVertical = (Input.GetAxis(leftStickVertical) * turnSpeed);
        leftStickInputHorizontal = (Input.GetAxis(leftStickHorizontal) * turnSpeed);
    }

    private void UpdateCurrentTilt()
    {
        currentVerticalTilt = gameObject.transform.rotation.eulerAngles.x;

        currentHorizontalTilt = gameObject.transform.rotation.eulerAngles.y;

        Debug.Log("X tilt is " + currentVerticalTilt + " Y tilt is " + currentHorizontalTilt);
    }

    private void AccelerateForward()
    {
        playerRigidBody.AddRelativeForce(zeroConstant, zeroConstant, accelerationSpeed);
    }

    //TODO: gameObject will turn to max turn angle, but won't turn back once it has reached that position
    private void Turn()
    {
        //if (currentVerticalTilt < maxVerticalTilt || currentVerticalTilt > ((rotationEulerConstant - maxVerticalTilt)))
        //{
        //    gameObject.transform.Rotate(leftStickInputVertical, zeroConstant, zeroConstant);
        //}

        //if (currentHorizontalTilt < maxHorizontalTilt || currentHorizontalTilt > (rotationEulerConstant - maxHorizontalTilt))
        //{
        //    gameObject.transform.Rotate(zeroConstant, leftStickInputHorizontal, zeroConstant);
        //}

        gameObject.transform.Rotate(zeroConstant, leftStickInputHorizontal, zeroConstant);
        gameObject.transform.Rotate(leftStickInputVertical, zeroConstant, zeroConstant);

        Debug.Log("X input is " + leftStickInputVertical + " Y input is " + leftStickInputHorizontal);
    }
}
