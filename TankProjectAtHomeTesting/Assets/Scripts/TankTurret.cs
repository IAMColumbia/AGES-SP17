using UnityEngine;
using System.Collections;
using System;

public class TankTurret : MonoBehaviour 
{
    [SerializeField]
    private float rotationSpeed = 50;

    private float rotateLeftInput;
    private float rotateRightInput;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
        GetInput();
	}

    private void GetInput()
    {
        rotateLeftInput = Input.GetAxis("RotateTurretLeft");
    }

    private void FixedUpdate()
    {
        // TODO: Do we need to use rigidbody so that rotating turret can't push heavy enemy tanks, etc?
        // TODO: Implement rotate right
        transform.Rotate(rotateLeftInput * rotationSpeed * Time.deltaTime * Vector3.down);
    }
}
