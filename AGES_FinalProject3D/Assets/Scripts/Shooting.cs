using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class Shooting : MonoBehaviour
{
    [SerializeField]
    private string shootButton;

    [SerializeField]
    LayerMask layerToCheckForEnemies;
    [SerializeField]
    private Transform shootingPosition;
    [SerializeField]
    private float shootingReticleSpeed;
    [SerializeField]
    private float shootingDurationSeconds;

    [SerializeField]
    private Color noEnemyColor;
    [SerializeField]
    private Color yesEnemyColor;

    private float maxDistanceToActivate = 10;
    private const float zeroConstant = 0;
    private bool isShooting = false;

    private ParticleSystem laserParticles;
    private AudioSource laserAudio;
    private WaitForSeconds shootingDuration;
    private Image reticleImage;

	// Use this for initialization
	void Start ()
    {
        laserParticles = GetComponentInChildren<ParticleSystem>();
        laserAudio = GetComponent<AudioSource>();
        reticleImage = GetComponentInChildren<Image>();
        shootingDuration = new WaitForSeconds(shootingDurationSeconds);
	}
	
	// Update is called once per frame
	void Update ()
    {
        UpdateReticleColor();
	}

    void FixedUpdate()
    {
        Shoot();
        ShootingAudio();
    }

    void Shoot()
    {
        if (Input.GetButtonDown(shootButton))
        {
            StartCoroutine(ShootLaser());
        }
    }

    private void UpdateReticleColor()
    {
        Vector3 endpoint = shootingPosition.position;

        RaycastHit raycastHit;

        Health enemyHealth;

        //Checks to see if an enemy is in range
        if (Physics.Raycast(transform.position, transform.forward, out raycastHit, maxDistanceToActivate, layerToCheckForEnemies))
        {
            enemyHealth = raycastHit.transform.gameObject.GetComponent<Health>();
        }
        else
        {
            enemyHealth = null;
        }

        //colors the reticle based on if an enemy is in range or not
        if (enemyHealth == null)
        {
            //color it green
            reticleImage.color = noEnemyColor;
        }
        else
        {
            //color it red
            reticleImage.color = yesEnemyColor;
        }
    }

    private IEnumerator ShootLaser()
    {
        Vector3 endpoint = shootingPosition.position;

        RaycastHit raycastHit;

        Health enemyHealth;

        isShooting = true;
        laserParticles.gameObject.SetActive(true);
        laserParticles.Play();

        //Change max distance to activate float so it's at the same location as the reticle
        //Check about where the ray is being cast.
        if (Physics.Raycast(transform.position, transform.forward, out raycastHit, maxDistanceToActivate, layerToCheckForEnemies))
        {
            enemyHealth = raycastHit.transform.gameObject.GetComponent<Health>();

            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(1);
            }
        }

        yield return shootingDuration;

        laserParticles.gameObject.SetActive(false);
        laserParticles.Stop();
        isShooting = false;
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
