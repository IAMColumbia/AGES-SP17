using UnityEngine;
using System.Collections;

public class TankMovement : MonoBehaviour
{
    [SerializeField]
    private int playerNumber = 1;
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float turnSpeed = 180f;
    [SerializeField]
    private float boostMultiplier = 2f;
    [SerializeField]
    private int boostUseLimit = 3;

    private string movementAxisName;
    private string turnAxisName;
    private Rigidbody rigidBody;
    private float movementInputValue;
    private float turnInputValue;
    private bool canBoost;
    private bool isBoosting;
    private int boostsUsed = 0;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Use this for initialization
    private void Start ()
    {
        movementAxisName = "Vertical" + playerNumber;
        turnAxisName = "Horizontal" + playerNumber;
        canBoost = true;
	
	}
	
	// Update is called once per frame
	private void Update ()
    {
        movementInputValue = Input.GetAxis(movementAxisName);
        turnInputValue = Input.GetAxis(turnAxisName);
        StartCoroutine(Boost());
        CheckForAvailableBoosts();
	
	}

    private void FixedUpdate()
    {
        Move();
        Turn();
    }

    private void Move()
    {
        Vector3 movement = transform.forward * movementInputValue * speed * Time.deltaTime;
        rigidBody.MovePosition(rigidBody.position + movement);
        if(isBoosting)
        {
            rigidBody.MovePosition(rigidBody.position + movement * boostMultiplier);
        }
    }

    private void Turn()
    {
        float turn = turnInputValue * turnSpeed * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        rigidBody.MoveRotation(rigidBody.rotation * turnRotation);
    }

    private IEnumerator Boost()
    {
        if (Input.GetButtonDown("Boost" + playerNumber))
        {
            boostsUsed++;

            if(canBoost)
            {
                isBoosting = true;
                canBoost = false;
                yield return new WaitForSeconds(0.5f);
                isBoosting = false;
                canBoost = true;
            } 
        }
    }

    private void CheckForAvailableBoosts()
    {
        if(boostsUsed == boostUseLimit)
        {
            canBoost = false;
        }
    }
}
