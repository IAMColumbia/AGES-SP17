using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class TankJump : MonoBehaviour
{
    [SerializeField]
    private string jumpButton;
    [SerializeField]
    private float jumpSpeed;
    [SerializeField]
    private Transform groundDetectorPosition;
    [SerializeField]
    private Vector3 groundDetectorHalfExtents;
    [SerializeField]
    private LayerMask whatIsGround;

    private Rigidbody myRigidBody;
    private bool isOnGround;

    private float xConst = 0;
    private float yConst = 0;
    private float zConst = 0;

    // Use this for initialization
    void Start ()
    {
        myRigidBody = GetComponent<Rigidbody>();
        //for testing purposes, delete later
        //isOnGround = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
        UpdateIsOnGround();
        HandleJump();
	}

    private void HandleJump()
    {
        if (Input.GetAxis(jumpButton) >= 0.1 && isOnGround == true)
        {
            myRigidBody.AddRelativeForce(xConst,jumpSpeed,zConst);
        }
    }

    private void UpdateIsOnGround()
    {
        Collider[] groundObjectsArray = Physics.OverlapBox(groundDetectorPosition.position, groundDetectorHalfExtents, groundDetectorPosition.rotation, whatIsGround);

        isOnGround = (groundObjectsArray.Length > 0);
    }
}
