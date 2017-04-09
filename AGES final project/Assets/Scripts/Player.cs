using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 5f;
    [SerializeField]
    float jumpHeight = 5f;
    [SerializeField]
    LayerMask groundLayer;
    [SerializeField]
    Transform groundDetectPoint;
    [SerializeField]
    float groundDetectRadius = 0.2f;

    Rigidbody2D rigidBody2D;
    bool isOnGround;

	// Use this for initialization
	void Start ()
    {
        rigidBody2D = gameObject.GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        CheckIsOnGround();
        HandleMovement();
        HandleJump();
	}

    private void HandleMovement()
    {
        float moveInput = Input.GetAxis("Horizontal");
        float currentYVelocity = rigidBody2D.velocity.y;
        Vector2 velocityToSet = new Vector2(moveSpeed * moveInput, currentYVelocity);
        rigidBody2D.velocity = velocityToSet;
    }

    private void HandleJump()
    {
        if(Input.GetButtonDown("Jump") && isOnGround)
        {
            float currentXVelocity = rigidBody2D.velocity.x;
            Vector2 velocityToSet = new Vector2(currentXVelocity, jumpHeight);
            rigidBody2D.velocity = velocityToSet;
        }
    }

    private void CheckIsOnGround()
    {
        Collider2D[] groundObjects = Physics2D.OverlapCircleAll(groundDetectPoint.position, groundDetectRadius, groundLayer);
        isOnGround = groundObjects.Length > 0;
    }
}
