﻿using UnityEngine;
using System.Collections;

public class DamageBlocks : MonoBehaviour
{

    [SerializeField]
    float blockDamage;

    [SerializeField]
    float blockTimeAlive;

    [SerializeField]
    TankHealth targetHealth;

    [SerializeField]
    ParticleSystem tankParticleSystem;

    private void Update()
    {
        DestroyAfterDelay();
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

            tankParticleSystem.transform.parent = null;

            tankParticleSystem.Play();

            Destroy(tankParticleSystem.gameObject, tankParticleSystem.duration);
            Destroy(gameObject);
        }
    }

    private void DestroyAfterDelay()
    {
        Destroy(gameObject, blockTimeAlive);
    }
}
