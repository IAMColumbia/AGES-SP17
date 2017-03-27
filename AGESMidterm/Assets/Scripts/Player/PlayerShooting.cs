using UnityEngine;
using System.Collections;
using System;

public class PlayerShooting : MonoBehaviour {

    public float PlayerNumber = 1;
    public Rigidbody Bullet;
    public Transform BulletSpawnTransform;
    public AudioSource ShootingAudio;
    public float launchForce;

    private string fireInputButton;
    private float fireRate = .5f;
    private float nextFire = 0;
	
	void Update () {

        GetAxis();

        if(Input.GetButton(fireInputButton) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;

            Rigidbody createBullet = Instantiate(Bullet, BulletSpawnTransform.position, BulletSpawnTransform.rotation) as Rigidbody;

            createBullet.velocity = launchForce * BulletSpawnTransform.transform.forward;
            ShootingAudio.Play();
        }

	}

    private void GetAxis()
    {
        fireInputButton = "Fire" + PlayerNumber;
    }
}
