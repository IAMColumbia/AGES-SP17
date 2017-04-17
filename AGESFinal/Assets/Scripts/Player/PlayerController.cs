using UnityEngine;
using System.Collections;
using System;

public class PlayerController : MonoBehaviour {

    public float playerNumber;

    [SerializeField]
    private float movementSpeed;

    [SerializeField]
    private float jumpForce;

    [SerializeField]
    private LayerMask groundLayer;


    private float HorizontalAxis;
    private float overlapSphereRadius = 1.4f;
    private float clampMaxRigidbodySpeed = 10;
    private float clampMaxRigidbodyJumpHeight = 15;


    private bool grounded = false;


    private Transform jumpPoint;
    private Rigidbody2D rBody2D;
    private Animator anim;
    private ParticleSystem particleSystem;

    void Start () {

        particleSystem = GetComponentInChildren<ParticleSystem>();
        jumpPoint = GetComponentInChildren<Transform>();
        rBody2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        particleSystem.gameObject.SetActive(false);
	}

    private void Update()
    {
        GetAxis();
        HandleMovement();
        HandleJump();
        
    }

    private void HandleMovement()
    {


        if (grounded && Input.GetButton("Duck" + playerNumber))
        {

            anim.SetBool("isDucking", true);
            
        }
        else
        {
            anim.SetFloat("HorizontalSpeed", Mathf.Abs(HorizontalAxis));

            anim.SetBool("isDucking", false);

            rBody2D.velocity = new Vector2(HorizontalAxis * movementSpeed, rBody2D.velocity.y);

            if (rBody2D.velocity.x > 0.1f || rBody2D.velocity.x < -0.1f)
                particleSystem.gameObject.SetActive(true);
            else
                particleSystem.gameObject.SetActive(false);

            HandleFlipSpriteCondition();

        }

    }

    private void HandleJump()
    {

        grounded = Physics2D.OverlapCircle(jumpPoint.position, overlapSphereRadius, groundLayer);

        anim.SetBool("Ground", grounded);

        anim.SetFloat("VerticalSpeed", rBody2D.velocity.y);

        if (grounded && Input.GetButtonDown("Jump" + playerNumber))
        {
            anim.SetBool("Ground", false);

            rBody2D.AddForce(new Vector2(0, jumpForce));
        }
    }

    private void HandleFlipSpriteCondition()
    {
        if (HorizontalAxis > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (HorizontalAxis < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
            GetComponent<SpriteRenderer>().flipX = false;
    }

    private void GetAxis()
    {
        HorizontalAxis = Input.GetAxis("Horizontal" + playerNumber);
    }
}
