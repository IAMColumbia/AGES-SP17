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
    private string lastDamageID;
    private bool isDead = false;
    private TankDamageState currentDamageState = TankDamageState.NoDamage;

    public event Action CriticalDamageReceived;
    public static event Action<Player> TankDestroyed;

    // IDamageable implementation
    public void TakeDamage(float amount, string id, IDamageSource source)
    {
        if (lastDamageID != id)
        {
            lastDamageID = id;
            currentHealth -= amount;
            Debug.Log("Took damage. Current health: " + currentHealth);
            UpdateDamageState();
            UpdateHealthVFX(currentHealth);

            if (currentHealth <= 0 && !isDead)
            {
                Die(source.ControllingPlayer);
            }
        }
    }

    private void UpdateDamageState()
    {
        float damageTaken = maxHealth - currentHealth;

        if (damageTaken >= maxHealth)
        {
            currentDamageState = TankDamageState.CriticalDamage;
        }
        else if (damageTaken >= heavyDamageThreshhold)
        {
            currentDamageState = TankDamageState.HeavyDamage;
        }
        else if (damageTaken >= mediumDamageThreshhold)
        {
            currentDamageState = TankDamageState.MediumDamage;
        }
        else if (damageTaken >= lightDamageThreshhold)
        {
            currentDamageState = TankDamageState.LightDamage;
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

    private void Die(Player playerThatKilled)
    {
        isDead = true;
        if (TankDestroyed != null)
        {
            TankDestroyed.Invoke(playerThatKilled);
            CriticalDamageReceived.Invoke();
        }
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
           // Debug.Log(particleSystem.isPlaying);
        }

        // For stopping stubborn particles. Not sure why they are stubborn.
        StartCoroutine(StopParticlesAfterDelay());
    }


    // I honestly have no clue why, but for somereason these two groups
    // of particle systems don't stop unless I call it twice...
    private IEnumerator StopParticlesAfterDelay()
    {
        yield return new WaitForEndOfFrame();
        foreach (var particleSystem in criticalDamageParticleSystems)
        {
            particleSystem.Stop();
            //Debug.Log(particleSystem.isPlaying);
        }
        foreach (var particleSystem in lightDamageParticleSystems)
        {
            particleSystem.Stop();
        }
    }

}
