using UnityEngine;
using System.Collections;
using System;

public class PlayerMovement : MonoBehaviour
{

    #region Fields

    Rigidbody rigidbody;

    Renderer renderer;

    bool isReadyToTimeJump;

    bool isTimeJumping;

    bool isJumping;

    bool isUsingCustomGravity;

    float movementSpeed;

    float jumpVelocity;

    float gravityVelocity;

    float jumpLength;

    float timeJumpCooldown;

    float maxVelocity;

    float velocityDampening;

    bool isBoosting;

    float boostDuration;

    float boostPower;

    bool isAlive;

    #endregion

    #region Serialized Fields

    [SerializeField]
    GameObject spawn;

    [SerializeField]
    GameObject canvas;

    [SerializeField]
    Material[] materials;

    #endregion

    [HideInInspector]
    public AudioSource audiosource;

    [SerializeField]
    public AudioClip[] clip;

    bool PlayerReady;

    // Use this for initialization
    void Start ()
    {
        #region Set Fields

        setFields();

        #endregion
    }

    private void setFields()
    {
        isAlive = true;

        rigidbody = GetComponent<Rigidbody>();
        renderer = GetComponent<Renderer>();

        isReadyToTimeJump = true;
        isTimeJumping = false;

        movementSpeed = 250f;
        jumpVelocity = 10f;
        gravityVelocity = 15f;
        jumpLength = .75f;
        maxVelocity = 500f;
        velocityDampening = 0.5f;

        timeJumpCooldown = 0.25f;

        boostDuration = 1f;
        boostPower = 2;

        gameObject.layer = 8;

        audiosource = GetComponent<AudioSource>();

        PlayerReady = true;
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        if (PlayerReady == false)
        {
            setFields();
        }

        PlayerInput();

        PlayerPhysics();
    }

    #region Player Input

    //Checks for Player Input
    private void PlayerInput()
    {
        if (isAlive)
        {
            Move();
            Jumps();
        }

        if (!isAlive)
        {
            if (Input.GetButtonDown("Jump"))
            {
                Respawn();
            }
        }
    }

    //Returns the player their last checkpoint and resets their state
    private void Respawn()
    {
        isAlive = true;
        transform.position = spawn.transform.position;
        renderer.material = materials[0];
        canvas.SetActive(false);
    }

    //Allows the player to jump and time jump
    private void Jumps()
    {
        if (Input.GetButtonDown("Jump") && !isJumping && !isTimeJumping && !isBoosting)
        {
            StartCoroutine(ActivateJump());

            float xVelocity = rigidbody.velocity.x;
            float yVelocity = jumpVelocity;

            Vector3 velocityToSet = new Vector3(xVelocity, yVelocity, 0);

            rigidbody.velocity = velocityToSet;
        }

        if (Input.GetButtonDown("Vertical") && isReadyToTimeJump)
        {
            StartCoroutine(ActivateTimeJump());
        }
    }

    #region Jump Coroutines

    IEnumerator ActivateTimeJump()
    {
        Debug.Log("JUMP BEGIN");

        renderer.material = materials[1];
        gameObject.layer = 9;

        audiosource.volume = 0.5f;
        audiosource.clip = clip[0];
        audiosource.Play();

        rigidbody.useGravity = false;
        isReadyToTimeJump = false;
        isTimeJumping = true;

        Vector3 storedVelocity = rigidbody.velocity;
        Vector3 storedAngularVelocity = rigidbody.angularVelocity;

        while (isTimeJumping)
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;

            //yield return new WaitForSeconds(timeJumpLength);
            yield return new WaitUntil(() => Input.GetButton("Vertical") == false);


            isTimeJumping = false;
            Debug.Log("JUMP END");
        }

        renderer.material = materials[0];
        gameObject.layer = 8;

        rigidbody.useGravity = true;
        rigidbody.velocity = storedVelocity;
        rigidbody.angularVelocity = storedAngularVelocity;

        StartCoroutine(ActivateBoost());
    }

    IEnumerator ActivateBoost()
    {

        isBoosting = true;

        while (isBoosting)
        {
            rigidbody.velocity = (rigidbody.velocity) * boostPower;
            rigidbody.angularVelocity = (rigidbody.angularVelocity) * boostPower;

            yield return new WaitForSeconds(boostDuration);
            yield return new WaitUntil(() => isJumping == false);

            isBoosting = false;
        }

        Debug.Log("Cooldown start");

        yield return new WaitForSeconds(timeJumpCooldown);

        isReadyToTimeJump = true;

        Debug.Log("Cooldown finish");
    }

    IEnumerator ActivateJump()
    {
        isJumping = true;

        isUsingCustomGravity = false;

        yield return new WaitForSeconds(jumpLength);

        isUsingCustomGravity = true;
    }

    #endregion

    //Moves the player
    private void Move()
    {
        if (!isTimeJumping && !isBoosting)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);

            float movementInput = Input.GetAxis("Horizontal");

            float xVelocity = (movementInput * Time.deltaTime * movementSpeed);
            float yVelocity = rigidbody.velocity.y;

            Vector3 velocityToSet = new Vector3(xVelocity, yVelocity, 0);
            

            rigidbody.velocity = velocityToSet;
        }
    }

    #endregion

    #region Player Physics

    //Calls player physics controlling methods
    private void PlayerPhysics()
    {
        LimitVelocity();
        Gravity();
    }

    //Limits the player's velocity
    private void LimitVelocity()
    {
        if (rigidbody.velocity.sqrMagnitude > maxVelocity)
        {
            rigidbody.velocity *= velocityDampening;
        }
    }

    //Gives the player custom gravity
    private void Gravity()
    {
        if (isUsingCustomGravity && !isTimeJumping)
        {
            Vector3 gravity = new Vector3(0, (Time.deltaTime * gravityVelocity * -1), 0);

            rigidbody.AddForce(gravity);
        }
    }

    #endregion

    #region Collisions / Damage

    //Resets jumps on the ground, kills the player on hazards
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isJumping = false;
        }

        if (collision.gameObject.tag == "Hazard")
        {
            Die();
        }
    }

    //Sets the players checkpoint
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "CheckPoint")
        {
            spawn = other.gameObject;
        }
    }

    //Lets the player die from bombs
    public void BlowUp()
    {
        Die();
    }

    //Kills the player and player input
    public void Die()
    {
        if (isAlive == true)
        {
            isAlive = false;
            renderer.material = materials[2];
            canvas.SetActive(true);

            audiosource.volume = 1f;
            audiosource.clip = clip[1];
            audiosource.Play();
        }
    }

    #endregion
}
