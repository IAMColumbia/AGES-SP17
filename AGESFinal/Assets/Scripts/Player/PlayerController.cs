﻿using UnityEngine;
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
    private float JumpAxis;
    private float overlapSphereRadius = 1.4f;
    private float clampMaxRigidbodySpeed = 10;
    private float clampMaxRigidbodyJumpHeight = 15;


    private bool grounded = false;
    private bool facingRight = true;


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
        Debug.Log(rBody2D.velocity.x);
    }

    private void HandleMovement()
    {

        anim.SetFloat("HorizontalSpeed", Mathf.Abs(HorizontalAxis));

        rBody2D.velocity = new Vector2(HorizontalAxis * movementSpeed, rBody2D.velocity.y);

        if (rBody2D.velocity.x > 0.1f || rBody2D.velocity.x < -0.1f)
            particleSystem.gameObject.SetActive(true);
        else
            particleSystem.gameObject.SetActive(false);

        HandleFlipSpriteCondition();

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
        if (HorizontalAxis > 0 && !facingRight)
            FlipSprite();
        else if (HorizontalAxis < 0 && facingRight)
            FlipSprite();
    }

    private void FlipSprite()
    {
        facingRight = !facingRight;
        Vector3 parentScale = transform.localScale;
        parentScale.x *= -1;
        transform.localScale = parentScale;
    }

    private void GetAxis()
    {
        HorizontalAxis = Input.GetAxis("Horizontal" + playerNumber);
        JumpAxis = Input.GetAxisRaw("Jump" + playerNumber);
    }
}
