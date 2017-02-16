using UnityEngine;
using System.Collections;
using System;
using UnityStandardAssets.Effects;
using System.Collections.Generic;

interface IDamageable
{
    void TakeDamage(float amount);
}

public class TankHealth : MonoBehaviour, IDamageable {
    public float CurrentHealth { get; private set; }

    public bool Alive { get; private set; }

    [SerializeField]
    float MaxHealth = 2000;

    [SerializeField]
    MultiParticleSystem lightDamageParticles, mediumDamageParticles, heavyDamageParticles, criticalDamageParticles;

    float lightDamage = 0.1f;
    float mediumDamage = 0.5f;
    float heavyDamage = 0.75f;
    float criticalDamage = 0.99f;

    Dictionary<float, MultiParticleSystem> DamageLevels = new Dictionary<float, MultiParticleSystem>();

    // Use this for initialization
    void Start () {
        CurrentHealth = MaxHealth;
        Alive = true;

        DamageLevels.Add(lightDamage, lightDamageParticles);
        DamageLevels.Add(mediumDamage, mediumDamageParticles);
        DamageLevels.Add(heavyDamage, heavyDamageParticles);
        DamageLevels.Add(criticalDamage, criticalDamageParticles);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Die()
    {
        Alive = false;
    }

    public void TakeDamage(float amount)
    {
        CurrentHealth -= amount;
        Debug.Log("CURRENT HEALTH: " + CurrentHealth);
        UpdateHealthVFX();

        if(CurrentHealth <= 0 && Alive)
        {
            Die();
        }
    }

    void UpdateHealthVFX()
    {
        float damage = 1 - (CurrentHealth / MaxHealth);

        foreach(KeyValuePair<float, MultiParticleSystem> damageLevel in DamageLevels)
        {
            if(damage >= damageLevel.Key)
            {
                damageLevel.Value.Play();
            }
        }
    }
}
