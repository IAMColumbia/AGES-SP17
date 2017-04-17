using UnityEngine;
using System.Collections;
using System;

public class EnemyShooting : MonoBehaviour
{
    [SerializeField]
    private float shootingDelayInFloat;
    [SerializeField]
    private LayerMask layerToCheckForPlayer;
    private GameObject playerToShootAt;

    private SphereCollider activationZone;

    private AudioSource laserAudio;

    private bool isShooting;

    private WaitForSeconds shootingDelay;

	// Use this for initialization
	void Start ()
    {
        playerToShootAt = GameObject.Find("Player");
        laserAudio = GetComponent<AudioSource>();
        shootingDelay = new WaitForSeconds(shootingDelayInFloat);
        StartCoroutine(ShootOnTimer());
	}

    //shoots character on a timer
    private IEnumerator ShootOnTimer()
    {
        while (playerToShootAt != null)
        {
            Shoot();
            yield return shootingDelay;
        }
    }

    private void Shoot()
    {
        //declare shooting endpoint
        Vector3 endPoint;

        RaycastHit raycastHit;

        Health playerHealth;
    }

    // Update is called once per frame
    void Update ()
    {
        LookAtPlayer();
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
