using UnityEngine;
using System.Collections;
using System;

public class EnemyShooting : MonoBehaviour
{
    [SerializeField]
    private float shootingDelayInFloat;
    [SerializeField]
    private LayerMask layerToCheckForPlayer;
    [SerializeField]
    private float shootRange;

    private GameObject playerToShootAt;

    private SphereCollider activationZone;

    private AudioSource laserAudio;

    private bool isShooting;

    private WaitForSeconds shootingDelay;

    private ParticleSystem laserParticles;

	// Use this for initialization
	void Start ()
    {
        //playerToShootAt = GameObject.Find("Player");
        laserAudio = GetComponent<AudioSource>();
        shootingDelay = new WaitForSeconds(shootingDelayInFloat);
        laserParticles = GetComponentInChildren<ParticleSystem>();
        StartCoroutine(ShootOnTimer());
    }

    // Update is called once per frame
    void Update()
    {
        CheckActivationZoneForPlayer();
        LookAtPlayer();
    }

    private void CheckActivationZoneForPlayer()
    {
        Collider[] activateZoneArray = Physics.OverlapSphere(gameObject.transform.position, 10,layerToCheckForPlayer);

        foreach (Collider player in activateZoneArray)
        {
            playerToShootAt = player.gameObject;
        }
    }

    //shoots character on a timer
    private IEnumerator ShootOnTimer()
    {
        while (Time.timeScale == 1)
        {
            yield return shootingDelay;
            while (playerToShootAt != null)
            {
                TurnOnLaser();
                yield return shootingDelay;
                TurnOffLaser();
                yield return shootingDelay;
            }
        }
    }


    private void TurnOnLaser()
    {
        //declare shooting endpoint
        Vector3 endpoint = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + shootRange);

        RaycastHit raycastHit;

        Health playerHealth;

        isShooting = true;
        laserParticles.gameObject.SetActive(true);
        laserParticles.Play();

        //Change max distance to activate float so it's at the same location as the reticle
        //Check about where the ray is being cast.
        if (Physics.Raycast(transform.position,transform.forward, out raycastHit,shootRange,layerToCheckForPlayer))
        {
            playerHealth = raycastHit.transform.gameObject.GetComponent<Health>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(1);
            }
        }
    }

    private void TurnOffLaser()
    {
        laserParticles.gameObject.SetActive(false);
        laserParticles.Stop();
        isShooting = false;
    }

    private void LookAtPlayer()
    {
        gameObject.transform.LookAt(playerToShootAt.transform);
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
