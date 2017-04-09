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
        MoveShipPosition();
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

    private void MoveShipPosition()
    {
        gameObject.transform.Translate(leftStickInputHorizontal, leftStickInputVertical, zeroConstant, Space.World);

        Debug.Log("X input is " + leftStickInputVertical + " Y input is " + leftStickInputHorizontal);
    }
}
