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

    GameManager gameManager;
    Rigidbody2D rigidBody2D;
    SpriteRenderer spriteRenderer;
    Animator animator;

    float deathMoveSpeed = 0f;
    float deathJumpHeight = 0f;
    bool isOnGround;

    public bool isAlive;

    // Use this for initialization
    void Start ()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        rigidBody2D = gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
        animator = gameObject.GetComponentInChildren<Animator>();

        isAlive = true;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(isAlive)
        {
            CheckIsOnGround();
            HandleMovement();
            HandleJump();
        }
        else
        {
            Death();
            CheckForReSpawn();
        }
	}

    private void HandleMovement()
    {
        //moveSpeed = moveSpeed;
        float moveInput = Input.GetAxis("Horizontal");
        animator.SetFloat("Speed", Mathf.Abs(moveInput));
        float currentYVelocity = rigidBody2D.velocity.y;
        Vector2 velocityToSet = new Vector2(moveSpeed * moveInput, currentYVelocity);
        rigidBody2D.velocity = velocityToSet;
        if(moveInput < 0)
        {

            spriteRenderer.flipX = true;
        }
        else if(moveInput > 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    private void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && isOnGround)
        {
            //jumpHeight = jumpHeight;
            animator.SetBool("IsOnGround", false);
            float currentXVelocity = rigidBody2D.velocity.x;
            Vector2 velocityToSet = new Vector2(currentXVelocity, jumpHeight);
            rigidBody2D.velocity = velocityToSet;
        }
        else if(isOnGround)
        {
            animator.SetBool("IsOnGround", true);
        }
        else if(isOnGround == false)
        {
            animator.SetBool("IsOnGround", false);
        }
    }

    private void CheckIsOnGround()
    {
        Collider2D[] groundObjects = Physics2D.OverlapCircleAll(groundDetectPoint.position, groundDetectRadius, groundLayer);
        isOnGround = groundObjects.Length > 0;
    }

    private void Death()
    {
        animator.SetBool("IsAlive", false);
        moveSpeed = deathMoveSpeed;
        jumpHeight = deathJumpHeight;
    }

    private void CheckForReSpawn()
    {
        if(!isAlive && Input.GetButtonDown("Jump"))
        {
            animator.SetBool("IsAlive", true);
            isAlive = true;
            this.gameObject.transform.position = gameManager.currentCheckpoint.position;
            moveSpeed = 5f;
            jumpHeight = 5f;
        }
    }
}
