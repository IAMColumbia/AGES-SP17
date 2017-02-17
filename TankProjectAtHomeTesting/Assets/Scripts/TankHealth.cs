using UnityEngine;
using System.Collections;
using System;

public class TankHealth : MonoBehaviour, IDamageable 
{
    private enum TankDamageState
    {
        NoDamage,
        LightDamage,
        MediumDamage,
        HeavyDamage,
        CriticalDamage
    }

    [Tooltip("Critical damage threshold will be equal to this value.")]
    [SerializeField]
    float maxHealth = 100;

    [Tooltip("How much damage taken before we play light damage particles.")]
    [SerializeField]
    float lightDamageThreshhold = 10;

    [Tooltip("How much damage taken before we play medium damage particles.")]
    [SerializeField]
    float mediumDamageThreshhold = 50;

    [Tooltip("How much damage taken before we play heavy damage particles.")]
    [SerializeField]
    float heavyDamageThreshhold = 75;

    [SerializeField]
    ParticleSystem[] lightDamageParticleSystems;

    [SerializeField]
    ParticleSystem[] mediumDamageParticleSystems;

    [SerializeField]
    ParticleSystem[] heavyDamageParticleSystems;

    [SerializeField]
    ParticleSystem[] criticalDamageParticleSystems;

    private float currentHealth;
    private float lastDamageID;
    private TankDamageState currentDamageState = TankDamageState.NoDamage;

    public event Action CriticalDamageReceived;

    // IDamageable implementation
    public void TakeDamage(float amount, float id)
    {
        if (lastDamageID != id)
        {
            lastDamageID = id;
            currentHealth -= amount;
            Debug.Log("Took damage. Current health: " + currentHealth);
            UpdateDamageState();
            UpdateHealthVFX(currentHealth);

            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    private void UpdateDamageState()
    {
        // This logic does not support healing.
        if (currentHealth <= 0)
        {
            if (currentDamageState != TankDamageState.CriticalDamage)
            {
                currentDamageState = TankDamageState.CriticalDamage;
                if (CriticalDamageReceived != null)
                    CriticalDamageReceived.Invoke();
            }
        }
        else
        {
            switch (currentDamageState)
            {
                case TankDamageState.NoDamage:
                    if (maxHealth - currentHealth >= heavyDamageThreshhold)
                        currentDamageState = TankDamageState.HeavyDamage;

                    else if (maxHealth - currentHealth >= mediumDamageThreshhold)
                        currentDamageState = TankDamageState.MediumDamage;

                    else if (maxHealth - currentHealth >= lightDamageThreshhold)
                        currentDamageState = TankDamageState.LightDamage;
                        break;

                case TankDamageState.LightDamage:
                    if (maxHealth - currentHealth >= heavyDamageThreshhold)
                        currentDamageState = TankDamageState.HeavyDamage;

                    else if (maxHealth - currentHealth >= mediumDamageThreshhold)
                        currentDamageState = TankDamageState.MediumDamage;
                    break;

                case TankDamageState.MediumDamage:
                    if (maxHealth - currentHealth >= heavyDamageThreshhold)
                        currentDamageState = TankDamageState.HeavyDamage;
                    break;

                case TankDamageState.HeavyDamage:
                case TankDamageState.CriticalDamage:
                    break;
                default:
                    throw new System.Exception("Unsupported TankDamageState!");
            }
        }
    }

    private void UpdateHealthVFX(float currentHealth)
    {
        switch (currentDamageState)
        {
            case TankDamageState.NoDamage:
                break;
            case TankDamageState.LightDamage:
                foreach (var particleSystem in lightDamageParticleSystems)
                {
                    particleSystem.Play();
                }
                break;
            case TankDamageState.MediumDamage:
                foreach (var particleSystem in mediumDamageParticleSystems)
                {
                    particleSystem.Play();
                }
                break;
            case TankDamageState.HeavyDamage:
                foreach (var particleSystem in heavyDamageParticleSystems)
                {
                    particleSystem.Play();
                }
                break;
            case TankDamageState.CriticalDamage:
                foreach (var particleSystem in criticalDamageParticleSystems)
                {
                    particleSystem.Play();
                }
                break;
            default:
                throw new System.Exception("Unsupported TankDamageState!");
        }
    }

    private void Die()
    {
       
    }

    // Use this for initialization
    void Start () 
	{
        currentHealth = maxHealth;

        foreach (var particleSystem in lightDamageParticleSystems)
        {
            particleSystem.Stop();
        }
        foreach (var particleSystem in mediumDamageParticleSystems)
        {
            particleSystem.Stop();
        }
        foreach (var particleSystem in heavyDamageParticleSystems)
        {
            particleSystem.Stop();
        }
        foreach (var particleSystem in criticalDamageParticleSystems)
        {
            particleSystem.Stop();
        }
    }
	

}
