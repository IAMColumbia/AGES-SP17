using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour
{
    public bool canDamage;
    public float bulletDamage = 0.20f;
    public float bulletLifetime = 2f;

    // Use this for initialization
    void Start()
    {
        Invoke("Kill", 2f);
        canDamage = true;
    }

    void Kill()
    {
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        if ((collision.gameObject.tag == "P1" || collision.gameObject.tag == "P2") && canDamage == true)
        {
            Rigidbody targetRigidbody = collision.gameObject.GetComponent<Rigidbody>();

            IDamage hitPlayer = targetRigidbody.GetComponentInParent<IDamage>();
            canDamage = false;
            if (hitPlayer != null)
            {
                hitPlayer.TakeDamage(bulletDamage);
                canDamage = false;
                Destroy(gameObject);
            }
        }

        if (collision.gameObject.tag == "Armor" && canDamage == true)
        {
            Rigidbody targetRigidbody = collision.gameObject.GetComponent<Rigidbody>();

            IDamage hitPlayer = targetRigidbody.GetComponentInParent<IDamage>();
            canDamage = false;
            if (hitPlayer != null)
            {
                hitPlayer.TakeDamage(bulletDamage);
                canDamage = false;
                Destroy(gameObject);
            }
        }

        if (collision.gameObject.tag == "Bullet")
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Environment")
        {
            Destroy(gameObject);
        }
    }
}
