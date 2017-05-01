using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour {

    public float playerNumber;

    [SerializeField]
    private float movementSpeed;

    [SerializeField]
    private float jumpForce;

    [SerializeField]
    private LayerMask groundLayer;

    [SerializeField]
    private GameObject butt;

    


    private float HorizontalAxis;
    private float overlapSphereRadius = 1.4f;
    private float clampMaxRigidbodySpeed = 10;
    private float clampMaxRigidbodyJumpHeight = 15;
    private float timerStartDelay = 2;
    private float time = 5;


    private bool grounded = false;

    private Text deathTimer; 
    private Color lerpedcolor = Color.white;
    private Transform jumpPoint;
    private Rigidbody2D rBody2D;
    private Animator anim;
    private ParticleSystem particleSystem;

    private void Awake()
    {
        GameObject timerGameObject = GameObject.Find("P" + playerNumber + "DeathTimer");
        
        if (timerGameObject != null)
        {
            deathTimer = timerGameObject.GetComponent<Text>();
            deathTimer.color = new Color(255, 237, 0, 0);
        }
    }

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

        if (deathTimer != null) 
            DeathTimer();
    }

    private void DeathTimer()
    {
        float minutes = (int)time / 60;
        float seconds = (int)time % 60;
        deathTimer.text = minutes.ToString() + ":" + seconds.ToString("00");
        if (HorizontalAxis == 0)
        {
            timerStartDelay -= Time.deltaTime;

            if (timerStartDelay < 0)
            {
                deathTimer.color = new Color(255, 237, 0, 1);
                deathTimer.text = minutes.ToString() + ":" + seconds.ToString("00");
                lerpedcolor = Color.Lerp(Color.yellow, Color.red, Mathf.PingPong(Time.time, 1));
                deathTimer.color = lerpedcolor;

                time -= Time.deltaTime;

                if (time < 0)
                {
                    deathTimer.color = new Color(255, 237, 0, 0);
                    time = 5;
                    timerStartDelay = 5;
                    GetComponentInChildren<PlayerHealth>().CueDeath();
                }

            }
        }
        else
        {
            deathTimer.color = new Color(255, 237, 0, 0);
            time = 5;
            timerStartDelay = 5;
            deathTimer.text = minutes.ToString() + ":" + seconds.ToString("00");
        }
    }

    private void HandleMovement()
    {
        if (grounded && Input.GetAxis("Duck" + playerNumber) > .5f)
        {
            particleSystem.gameObject.SetActive(false);
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
            butt.GetComponent<BoxCollider2D>().offset = new Vector2(-1.5f, 0);
            butt.GetComponent<BoxCollider2D>().size = new Vector2(1, 1);
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (HorizontalAxis < 0)
        {
            butt.GetComponent<BoxCollider2D>().offset = new Vector2(1.5f, 0);
            butt.GetComponent<BoxCollider2D>().size = new Vector2(1, 1);
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            butt.GetComponent<BoxCollider2D>().offset = new Vector2(0, 0);
            butt.GetComponent<BoxCollider2D>().size = new Vector2(0, 0);
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    private void GetAxis()
    {
        HorizontalAxis = Input.GetAxis("Horizontal" + playerNumber);
    }
}
