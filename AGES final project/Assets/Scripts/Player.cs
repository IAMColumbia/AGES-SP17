using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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
    [SerializeField]
    GameObject respawnText;
    [SerializeField]
    Transform spritePosition;
    [SerializeField]
    AudioClip jumpSound;
    [SerializeField]
    AudioClip deathSound;
    [SerializeField]

    SwitchWorlds switchWorlds;
    AudioSource audioSource;
    GameManager gameManager;
    Rigidbody2D rigidBody2D;
    SpriteRenderer spriteRenderer;
    Animator animator;

    float aliveMoveSpeed;
    float aliveJumpHeight;
    float deathMoveSpeed = 0f;
    float deathJumpHeight = 0f;
    bool isOnGround;
    bool playDeathSoundOnce = true;
    bool canMove = true;
    bool canJump = true;

    public bool isAlive;

    // Use this for initialization
    void Start ()
    {
        switchWorlds = gameObject.GetComponentInChildren<SwitchWorlds>();
        audioSource = gameObject.GetComponent<AudioSource>();
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
        if(canMove)
        {
            aliveMoveSpeed = moveSpeed;
            float moveInput = Input.GetAxis("Horizontal");
            animator.SetFloat("Speed", Mathf.Abs(moveInput));
            float currentYVelocity = rigidBody2D.velocity.y;
            Vector2 velocityToSet = new Vector2(aliveMoveSpeed * moveInput, currentYVelocity);
            rigidBody2D.velocity = velocityToSet;
            if (moveInput < 0)
            {

                spriteRenderer.flipX = true;
            }
            else if (moveInput > 0)
            {
                spriteRenderer.flipX = false;
            }
        }
        else
        {
            rigidBody2D.velocity = new Vector2(0f, 0f);
        }

    }

    private void HandleJump()
    {
        if(canJump)
        {
            if (Input.GetButtonDown("Jump") && isOnGround)
            {
                audioSource.clip = jumpSound;
                audioSource.Play();
                aliveJumpHeight = jumpHeight;
                animator.SetBool("IsOnGround", false);
                float currentXVelocity = rigidBody2D.velocity.x;
                Vector2 velocityToSet = new Vector2(currentXVelocity, aliveJumpHeight);
                rigidBody2D.velocity = velocityToSet;
            }
            else if (isOnGround)
            {
                animator.SetBool("IsOnGround", true);
            }
            else if (isOnGround == false)
            {
                animator.SetBool("IsOnGround", false);
            }
        }

    }

    private void CheckIsOnGround()
    {
        Collider2D[] groundObjects = Physics2D.OverlapCircleAll(groundDetectPoint.position, groundDetectRadius, groundLayer);
        isOnGround = groundObjects.Length > 0;
    }

    private void Death()
    {
        if(playDeathSoundOnce)
        {
            audioSource.clip = deathSound;
            audioSource.Play();
            playDeathSoundOnce = false;
        }
        else
        {
            audioSource.Stop();
        }
        respawnText.SetActive(true);
        animator.SetBool("IsAlive", false);
        aliveMoveSpeed = deathMoveSpeed;
        aliveJumpHeight = deathJumpHeight;
    }

    private void CheckForReSpawn()
    {
        if(!isAlive && Input.GetButtonDown("Jump"))
        {
            respawnText.SetActive(false);
            animator.SetBool("IsAlive", true);
            isAlive = true;
            this.transform.position = gameManager.currentCheckpoint.position;
            aliveMoveSpeed = moveSpeed;
            aliveJumpHeight = jumpHeight;
            playDeathSoundOnce = true;
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.transform.tag == "MovingPlatform")
        {
            transform.parent = coll.transform;
        }
    }

    void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.transform.tag == "MovingPlatform")
        {
            transform.parent = null;
        }
    }

    public void DisablePlayer()
    {
        canMove = false;
        animator.SetFloat("Speed", 0f);
        canJump = false;
        switchWorlds.enabled = false; 
    }
}
