using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
    float moveHoriz;
    float moveVert;

    float mouseX;
    [SerializeField] float moveSpeed;

    Vector3 movement, rotation;

    [SerializeField]
    Rigidbody rb;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {

        moveHoriz = Input.GetAxis("Horizontal");
        moveVert = Input.GetAxis("Vertical");
        mouseX = Input.GetAxis("Mouse X");
        movement = new Vector3(moveHoriz, 0f, moveVert);
        rotation = new Vector3(0f, mouseX, 0f);

        transform.Translate(movement* moveSpeed * Time.deltaTime);
        //rb.velocity = movement * moveSpeed;


        this.transform.Rotate(rotation);
	}
}
