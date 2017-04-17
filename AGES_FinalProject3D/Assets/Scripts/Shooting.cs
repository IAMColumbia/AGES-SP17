using UnityEngine;
using System.Collections;
using System;

public class Shooting : MonoBehaviour
{
    [SerializeField]
    private string shootButton;
    [SerializeField]
    private string rightStickVertical;
    [SerializeField]
    private string rightStickHorizontal;

    [SerializeField]
    LayerMask layerToCheckForEnemies;
    [SerializeField]
    private Transform shootingPosition;
    [SerializeField]
    private float shootingReticleSpeed;

    private float maxDistanceToActivate = 10;
    private const float zeroConstant = 0;
    private float rightStickVerticalInput;
    private float rightStickHorizontalInput;
    private bool isShooting = false;

    private ParticleSystem laserParticles;
    private AudioSource laserAudio;

	// Use this for initialization
	void Start ()
    {
        laserParticles = GetComponentInChildren<ParticleSystem>();
        laserAudio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        Shoot();
        UpdateRightStickInput();
        ShootingAudio();
        MoveReticle();
	}

    private void UpdateRightStickInput()
    {
        rightStickHorizontalInput = Input.GetAxis(rightStickHorizontal) * shootingReticleSpeed;
        rightStickVerticalInput = Input.GetAxis(rightStickVertical) * shootingReticleSpeed;
    }

    private void MoveReticle()
    {
        gameObject.transform.Translate(rightStickHorizontalInput, rightStickVerticalInput, zeroConstant, Space.World);
        maxDistanceToActivate = shootingPosition.position.z - gameObject.transform.position.z;
    }

    void Shoot()
    {
        if (Input.GetButton(shootButton))
        {
            Vector3 endpoint = shootingPosition.position;

            RaycastHit raycastHit;

            Health enemyHealth;

            isShooting = true;
            laserParticles.gameObject.SetActive(true);
            laserParticles.Play();

            //Shooting works but drawline does not currently
            Debug.DrawLine(transform.position, endpoint, Color.green, 5);

            //Change max distance to activate float so it's at the same location as the reticle
            //Check about where the ray is being cast.
            if (Physics.Raycast(transform.position,transform.forward, out raycastHit,maxDistanceToActivate,layerToCheckForEnemies))
            {
                enemyHealth = raycastHit.transform.gameObject.GetComponent<Health>();

                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(1);
                }
            }
        }

        else
        {
            laserParticles.gameObject.SetActive(false);
            laserParticles.Stop();
            isShooting = false;
        }
    }

    private void ShootingAudio()
    {
        if (isShooting && laserAudio.isPlaying == false)
        {
            laserAudio.Play();
        }
        if (!isShooting)
        {
            laserAudio.Stop();
        }
    }
}
