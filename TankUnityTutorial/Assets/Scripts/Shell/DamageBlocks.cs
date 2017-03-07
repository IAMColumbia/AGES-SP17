using UnityEngine;
using System.Collections;

public class DamageBlocks : MonoBehaviour {

    [SerializeField]
    float blockDamage;

    [SerializeField]
    TankHealth targetHealth;


    private void Start()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        // Find all the tanks in an area around the shell and damage them.

        if (other.gameObject.tag == "Player")
        {
            // Rigidbody targetRigidbody = other.gameObject.GetComponent<Rigidbody>();

            // targetRigidbody.AddExplosionForce(m_ExplosionForce, transform.position, m_ExplosionRadius);

            TankHealth targetHealth = other.gameObject.GetComponent<TankHealth>();

            targetHealth.TakeDamage(blockDamage);

            Destroy(gameObject);           
        }       
    }
}
