using UnityEngine;
using System.Collections;
using System;

public class TankHealth : MonoBehaviour, IDamagable
{
    [SerializeField]
    float maxHealth = 100;
    [SerializeField]
    ParticleSystem stage1Damage;
    [SerializeField]
    ParticleSystem stage2Damage;
    [SerializeField]
    ParticleSystem stage3Damage;
    [SerializeField]
    ParticleSystem stage4Damage;

    float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage, Vector3 explosionForce)
    {
        currentHealth -= damage;

        if (currentHealth < (maxHealth - 10) && currentHealth > (maxHealth - 50))
        {
            stage1Damage.Play();
        }

        if (currentHealth < (maxHealth - 51) && currentHealth > (maxHealth - 74))
        {
            stage2Damage.Play();
        }

        if (currentHealth < (maxHealth - 75) && currentHealth > (maxHealth - 99))
        {
            stage3Damage.Play();
        }

        if (currentHealth < (maxHealth - 100))
        {
            stage4Damage.Play();
            IHeavyExplodableObject explodableObject = GetComponentInParent<IHeavyExplodableObject>();
            explodableObject.Explode(explosionForce);
        }
    }
}
