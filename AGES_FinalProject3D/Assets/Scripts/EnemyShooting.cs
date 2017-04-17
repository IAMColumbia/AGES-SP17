using UnityEngine;
using System.Collections;

public class EnemyShooting : MonoBehaviour
{
    private GameObject playerToShootAt;

    private SphereCollider activationZone;

    private AudioSource laserAudio;

    private bool isShooting;

	// Use this for initialization
	void Start ()
    {
        playerToShootAt = GameObject.Find("Player");
        laserAudio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        LookAtAndShootPlayer();
	}

    private void LookAtAndShootPlayer()
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
