using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float PlayerNumber;
    public float MoveSpeed;

    private float InputAxisX;
    private float InputAxisY;

    private Rigidbody rigidBody;

	void Start () {
        rigidBody = GetComponent<Rigidbody>();
	}
	
	void Update ()
    {
        GetAxis();

        Vector3 movement = new Vector3(InputAxisY, 0, InputAxisX);

        transform.Translate(movement * MoveSpeed * Time.deltaTime);
    }

    private void GetAxis()
    {
        InputAxisX = Input.GetAxisRaw("Horizontal" + PlayerNumber);
        InputAxisY = Input.GetAxisRaw("Vertical" + PlayerNumber);
    }
}
