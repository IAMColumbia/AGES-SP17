using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float forwardBulletForce = 100;
    [SerializeField]
    private float backwardBulletForce = -100;

    private Rigidbody bulletRigidBody;
    private AudioSource bulletShotSound;

    private const float xConst = 0;
    private const float yConst = 0;

    void Awake()
    {
        bulletRigidBody = GetComponent<Rigidbody>();
        bulletShotSound = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }

    public void LaunchBullet(float bulletForce)
    {
        //bulletShotSound.Play();
        bulletRigidBody.velocity = bulletForce * gameObject.transform.forward;
        //bulletRigidBody.AddForce(xConst, yConst, bulletForce);
    }
}
