using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Camera player1Camera;
    [SerializeField] float moveSpeed = 5;
    [SerializeField] Rigidbody2D rigidBody;
    [SerializeField] string attachButton = "YellowAttach";

    private Vector2 targetPosition;

	// Use this for initialization
	void Start ()
    {
        
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        MovePlayer();
	}

    void MovePlayer()
    {
        if(Input.GetMouseButton(0))
        {
            targetPosition = player1Camera.ScreenToWorldPoint(Input.mousePosition);
			transform.position = targetPosition;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag == "Holding Point")
        {
            if(Input.GetButton(attachButton))
            {
                transform.position = other.transform.position;
            }
        }
    }
}
