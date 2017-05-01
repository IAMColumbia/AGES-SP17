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
    private float xAxisMovementMultiplier;
    [SerializeField]
    private float yAxisMovementMultiplier;
    [SerializeField]
    private float normalAccelerationSpeed;
    [SerializeField]
    private float slowdownSpeed;
    [SerializeField]
    private float minSpeedinMPH;
    [SerializeField]
    private float maxSpeedinMPH;

    private Rigidbody playerRigidBody;
    private GameUI gameUI;

    private float leftStickInputVertical;
    private float leftStickInputHorizontal;
    private float inGameTurnMultiplier;
    private float currentAccelerationSpeed;

    private const float zeroConstant = 0;
    private const float rotationEulerConstant = 360;

	// Use this for initialization
	void Start ()
    {
        playerRigidBody = GetComponent<Rigidbody>();
        gameUI = GameObject.Find("GameUI").GetComponent<GameUI>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        UpdateTurnInput();
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

    private void AccelerateForward()
    {
        //playerRigidBody.AddRelativeForce(zeroConstant, zeroConstant, accelerationSpeed,ForceMode.Force);

        Vector3 velocityToSet = new Vector3((leftStickInputHorizontal * xAxisMovementMultiplier), (leftStickInputVertical*yAxisMovementMultiplier), currentAccelerationSpeed) * Time.deltaTime;

        playerRigidBody.velocity = velocityToSet;
        //Add condition so you can't slow down too much
        if (Input.GetButton(slowDownButton))
        {
            SlowDown();
            inGameTurnMultiplier = Mathf.Lerp(turnMultiplierWhenSlow, turnMultiplier, Time.deltaTime);
        }
        else
        {
            SetMovementSpeedBackToNormal();
            inGameTurnMultiplier = Mathf.Lerp(turnMultiplier, turnMultiplierWhenSlow,Time.deltaTime);
        }
    }

    private void SlowDown()
    {
        currentAccelerationSpeed = slowdownSpeed;
    }

    private void SetMovementSpeedBackToNormal()
    {
        currentAccelerationSpeed = normalAccelerationSpeed;
    }

    private void MoveRotation()
    {
        //gameObject.transform.Translate(leftStickInputHorizontal, leftStickInputVertical, zeroConstant, Space.World);

        Quaternion roatationToRotateTo = Quaternion.Euler(leftStickInputVertical, leftStickInputHorizontal, zeroConstant);

        playerRigidBody.MoveRotation(roatationToRotateTo);

        //Debug.Log("X input is " + leftStickInputVertical + " Y input is " + leftStickInputHorizontal);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "End")
        {
            gameUI.Win();
        }
    }

    private void OnDisable()
    {
        gameUI.Lose();
    }
}
