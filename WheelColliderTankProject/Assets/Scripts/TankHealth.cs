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

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth < (maxHealth - 10) && currentHealth > (maxHealth - 50))
        {
            stage1Damage.Play();
        }

        if (currentHealth < 100 && currentHealth > 50)
        {
            stage1Damage.Play();
        }

        if (currentHealth < 100 && currentHealth > 50)
        {
            stage1Damage.Play();
        }

        if (currentHealth < 100 && currentHealth > 50)
        {
            stage1Damage.Play();
        }
    }
}
