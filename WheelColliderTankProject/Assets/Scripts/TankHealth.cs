using UnityEngine;
using System.Collections;
using System;

public class TankHealth : MonoBehaviour, IDamageable, IHeavyExplodableObject
{
    [SerializeField]
    float tankCurHealth;

    [SerializeField]
    int numberOfShotsToKill;

    [SerializeField]
    ParticleSystem smokeParticles;

    [SerializeField]
    ParticleSystem[] fireParticles;

    float tankMaxHealth;
    float tankHealthFirstIndicator;
    float tankHealthSecondIndicator;
    float tankHealthThirdIndicator;
    float tankHealthFourthIndicator;

    [Tooltip("When hit by a shell, force is applied at this location to 'rock' the tank.")]
    [SerializeField]
    Transform explosionPoint;

    [Tooltip("When hit by a shell, we use this much force to 'rock' the tank.")]
    [SerializeField]
    float explosionForce = 7000000;

    private Rigidbody rigidbody_useThis;
    // Use this for initialization
    void Start ()
    {
        rigidbody_useThis = GetComponent<Rigidbody>();

        tankMaxHealth = tankCurHealth;
        tankHealthFirstIndicator = tankMaxHealth - (tankMaxHealth / 5);
        tankHealthSecondIndicator = tankHealthFirstIndicator - (tankMaxHealth / 5);
        tankHealthThirdIndicator = tankHealthSecondIndicator - (tankMaxHealth / 5);
        tankHealthFourthIndicator = tankHealthThirdIndicator - (tankMaxHealth / 5);
    }
	
	// Update is called once per frame
	void Update ()
    {
        UpdateTankVisualToMatchTankHealth();
        if (tankCurHealth <= 0)
            StartCoroutine(TankDeathExplosion());
	}

    IEnumerator TankDeathExplosion()
    {
        yield return null;
    }

    void UpdateTankVisualToMatchTankHealth()
    {
        int oneFourthSmokeParticlesLimit = 125;
        int fireParticlesMiddleLimit = 10;

        Debug.Log(tankCurHealth);

        if (tankCurHealth < tankHealthFirstIndicator && tankCurHealth > tankHealthSecondIndicator)
        {
            smokeParticles.maxParticles = oneFourthSmokeParticlesLimit;
            smokeParticles.Play();
        }
        else if (tankCurHealth < tankHealthSecondIndicator && tankCurHealth > tankHealthThirdIndicator)
        {
            smokeParticles.maxParticles = oneFourthSmokeParticlesLimit * 2;
            if (!smokeParticles.isPlaying)
                smokeParticles.Play();
            //fireParticles.maxParticles = fireParticlesMiddleLimit / 2;
            //fireParticles.Play();
        }
        else if (tankCurHealth < tankHealthThirdIndicator && tankCurHealth > tankHealthFourthIndicator)
        {
            smokeParticles.maxParticles = oneFourthSmokeParticlesLimit * 3;
            if (!smokeParticles.isPlaying)
                smokeParticles.Play();
            //fireParticles.maxParticles = fireParticlesMiddleLimit;
            //if (!fireParticles.isPlaying)
            //    fireParticles.Play();
        }
        else if (tankCurHealth < tankHealthFourthIndicator)
        {
            smokeParticles.maxParticles = oneFourthSmokeParticlesLimit * 4;
            if (!smokeParticles.isPlaying)
                smokeParticles.Play();
            //fireParticles.maxParticles = fireParticlesMiddleLimit * 2;
            //if (!fireParticles.isPlaying)
            //    fireParticles.Play();
        }
    }

    public void Damage()
    {
        tankCurHealth = tankCurHealth - (tankMaxHealth / numberOfShotsToKill);
    }

    public void Explode(Vector3 incomingProjectileDirection)
    {
        if (tankCurHealth > 0)
        {
            Vector3 explosionDirection = Vector3.up + incomingProjectileDirection;
            rigidbody_useThis.AddForceAtPosition(explosionForce * explosionDirection, explosionPoint.position);
        }
    }
}
