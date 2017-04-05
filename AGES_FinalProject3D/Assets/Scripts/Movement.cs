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

    private Rigidbody playerRigidBody;

    private float leftStickInputVertical;
    private float leftStickInputHorizontal;

    private float currentVerticalTilt;
    private float currentHorizontalTilt;

    private const float zeroConstant = 0;

	// Use this for initialization
	void Start ()
    {
        playerRigidBody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        UpdateTurnInput();
	}


    private void FixedUpdate()
    {
        Turn();
    }

    private void UpdateTurnInput()
    {
        leftStickInputVertical = (Input.GetAxis(leftStickVertical) * turnSpeed);
        leftStickInputHorizontal = (Input.GetAxis(leftStickHorizontal) * turnSpeed);
    }

    private void Turn()
    {
        //TODO: Update current vertical tilt
        if (currentVerticalTilt < maxVerticalTilt)
        {
            gameObject.transform.Rotate(leftStickInputVertical, zeroConstant, zeroConstant);
        }

        if (currentHorizontalTilt < maxHorizontalTilt)
        {
            gameObject.transform.Rotate(zeroConstant, leftStickInputHorizontal, zeroConstant);
        }
    }
}
