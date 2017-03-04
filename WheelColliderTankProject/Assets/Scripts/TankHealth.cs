using UnityEngine;
using System.Collections;
using System;

public class TankHealth : MonoBehaviour, IDamageable, IHeavyExplodableObject
{
    float secondsToWait = 2;
    private IEnumerator coroutine;

    [SerializeField]
    float maxHealth = 100f;
    [SerializeField]
    float currentHealth;

    [SerializeField]
    Vector3 incomingShell;

    [SerializeField]
    ParticleSystem lightDamageParticles;
    [SerializeField]
    ParticleSystem mediumDamageParticles;
    [SerializeField]
    ParticleSystem heavyDamageParticles;
    [SerializeField]
    ParticleSystem criticalDamageParticles;

    enum HealthStates { light, medium, heavy, critical};
    HealthStates currentDamageState;

    public void TakeDamage(float damageTaken)
    {
        currentHealth = currentHealth - damageTaken;

        if (currentHealth >= 100f)
        {
            currentDamageState = HealthStates.light;
        }
        else if (currentHealth <= 99f || currentHealth >= 75f)
            currentDamageState = HealthStates.medium;
        else if (currentHealth <= 74f || currentHealth >= 51f)
            currentDamageState = HealthStates.heavy;
        else if (currentHealth <= 50f || currentHealth >= 0f)
            currentDamageState = HealthStates.critical;

        UpdateDamageState();
    }

    public void UpdateDamageState()
    {
        switch (currentDamageState)
        {
            case HealthStates.light:
                print("You have taken light damage.");
                lightDamageParticles.Play();

                break;
            case HealthStates.medium:
                print("You have taken medium damage.");
                mediumDamageParticles.Play();

                break;
            case HealthStates.heavy:
                print("You have taken heavy damage.");
                heavyDamageParticles.Play();

                break;
            case HealthStates.critical:
                print("You have taken critical damage.");
                criticalDamageParticles.Play();

                coroutine = WaitAndExplode(secondsToWait);
                StartCoroutine(coroutine);

                break;
        }
    }
    
    public void Explode(Vector3 incomingProjectileDirection)
    {
<<<<<<< HEAD
        throw new NotImplementedException();
    }

    private IEnumerator WaitAndExplode(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            Debug.Log("Waiting for " + waitTime + " seconds.");
            Explode(incomingShell);
        }
    }  
}
=======
        currentHealth = maxHealth;	
	}
	
	// Update is called once per frame
	void Update ()
    {        
        //TakeDamage();
    }
}
>>>>>>> origin/FeagleyTest
