using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TankMovement : MonoBehaviour
{
    //the majority of this script was taken and modified from the TANKS! tutorial
    [SerializeField]
    private int playerNumber = 1;
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float turnSpeed = 180f;
    [SerializeField]
    private float boostMultiplier = 2f;
    [SerializeField]
    private int boostsAvailable = 3;
    [SerializeField]
    private Slider boostSlider;
    [SerializeField]
    private ParticleSystem boostParticle;
    [SerializeField]
    private AudioSource movementAudio;
    [SerializeField]
    private AudioSource boostAudio;
    [SerializeField]
    private AudioClip engineMovingAudio;
    [SerializeField]
    private AudioClip TankBoostAudio;

    private string movementAxisName;
    private string turnAxisName;
    private Rigidbody rigidBody;
    private float movementInputValue;
    private float turnInputValue;
    private bool canBoost;
    private bool isBoosting;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Use this for initialization
    private void Start ()
    {
        movementAxisName = "Vertical" + playerNumber;
        turnAxisName = "Horizontal" + playerNumber;
        canBoost = true;
	
	}
	
	// Update is called once per frame
	private void Update ()
    {
        movementInputValue = Input.GetAxis(movementAxisName);
        turnInputValue = Input.GetAxis(turnAxisName);
        CheckForAvailableBoosts();
        boostSlider.value = boostsAvailable;
        EngineAudio();
	}

    private void FixedUpdate()
    {
        Move();
        Turn();
        StartCoroutine(Boost());
    }

    private void EngineAudio()
    {
        if (Mathf.Abs(movementInputValue) < 0.1f && Mathf.Abs(turnInputValue) < 0.1f)
        {
            if (movementAudio.clip == engineMovingAudio)
            {
                movementAudio.clip = null;
                movementAudio.Play();
            }
        }
        else
        {
            if (movementAudio.clip == null)
            {
                movementAudio.clip = engineMovingAudio;
                movementAudio.Play();
            }
        }
    }

    private void Move()
    {
        Vector3 movement = transform.forward * movementInputValue * speed * Time.deltaTime;
        rigidBody.MovePosition(rigidBody.position + movement);
        if(isBoosting)
        {
            rigidBody.MovePosition(rigidBody.position + movement * boostMultiplier);
        }
    }

    private void Turn()
    {
        float turn = turnInputValue * turnSpeed * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        rigidBody.MoveRotation(rigidBody.rotation * turnRotation);
    }

    private IEnumerator Boost()
    {
        if (Input.GetButtonDown("Boost" + playerNumber))
        {
            boostsAvailable--;

            if(canBoost)
            {
                boostAudio.clip = TankBoostAudio;
                boostAudio.Play();
                isBoosting = true;
                boostParticle.Play();
                canBoost = false;
                yield return new WaitForSeconds(0.5f);
                isBoosting = false;
                canBoost = true;
            } 
        }
    }

    private void CheckForAvailableBoosts()
    {
        if (boostsAvailable == 0)
        {
            canBoost = false;
        }
    }
}
