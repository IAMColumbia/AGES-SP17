using UnityEngine;
using System.Collections;

public class RoombaMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 10000;
    [SerializeField] float rotationSpeed = 10000;
    [SerializeField] string roombaMovement = "RoombaMovement_P1";
    [SerializeField] string roombaRotation = "RoombaRotation_P1";

	private Rigidbody2D roombaRigidBody;
	private GameObject gameManager;
	private GameManager gameManagerScript;
	public bool isMoving = false;

	// Use this for initialization
	void Start ()
    {
		roombaRigidBody = GetComponent<Rigidbody2D> ();
		gameManager = GameObject.Find ("Game Manager");
		gameManagerScript = gameManager.GetComponent<GameManager> ();
	}
	
	// Update is called once per frame
	void Update ()
    {
        
	}

    void FixedUpdate()
    {
        DoRoombaMovement();
    }

    void DoRoombaMovement()
    {
        float movementVertical = Input.GetAxis(roombaMovement);
        float movementRotation = Input.GetAxis(roombaRotation);
		Vector3 forward = transform.TransformDirection (Vector3.up);

		if (movementVertical > 0.1 && gameManagerScript.balloonList.Count > 1) 
		{
			roombaRigidBody.AddForce (forward * movementSpeed * Time.deltaTime);
			isMoving = true;
		} 
		else if (movementVertical < -0.1 && gameManagerScript.balloonList.Count > 1) 
		{
			roombaRigidBody.AddForce (forward * -movementSpeed * Time.deltaTime);
			isMoving = true;
		} 
		else if (movementRotation > 0.1 && gameManagerScript.balloonList.Count > 1) 
		{
			roombaRigidBody.AddTorque (-rotationSpeed * Time.deltaTime);
			isMoving = true;
		} 
		else if (movementRotation < -0.1 && gameManagerScript.balloonList.Count > 1) 
		{
			roombaRigidBody.AddTorque (rotationSpeed * Time.deltaTime);
			isMoving = true;
		} 
		else 
		{
			isMoving = false;
		}
    }
}
