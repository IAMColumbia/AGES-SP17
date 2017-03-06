using UnityEngine;
using System.Collections;

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

        smokeParticles.Stop();
        foreach(ParticleSystem ps in fireParticles)
        {
            ps.Stop();
        }
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
        int timeToWait = 2;
        Vector3 explosionDirection = Vector3.up;
        rigidbody_useThis.AddForceAtPosition(explosionForce * explosionDirection, explosionPoint.position);
        
        yield return new WaitForSeconds(timeToWait);

        rigidbody_useThis.mass = 100;
        explosionDirection = new Vector3(Random.Range(-359, 360), Random.Range(-359, 360), Random.Range(-359, 360));
        rigidbody_useThis.AddForceAtPosition(explosionForce * explosionDirection, explosionPoint.position);
    }

    void UpdateTankVisualToMatchTankHealth()
    {
        int oneFourthSmokeParticlesLimit = 125;
        int fireParticlesMiddleLimit = 10;
        int firstFPtoPlay = 0;
        int secondFPtoPlay = 0;
        int thirdFPtoPlay = 0;
        int fourthFPtoPlay = 0;
        int fifthFPtoPlay = 0;

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
            firstFPtoPlay = Random.Range(0, 5);
            fireParticles[firstFPtoPlay].maxParticles = fireParticlesMiddleLimit / 2;
            fireParticles[firstFPtoPlay].Play();
        }
        else if (tankCurHealth < tankHealthThirdIndicator && tankCurHealth > tankHealthFourthIndicator)
        {
            smokeParticles.maxParticles = oneFourthSmokeParticlesLimit * 3;
            if (!smokeParticles.isPlaying)
                smokeParticles.Play();
            do
            {
                secondFPtoPlay = Random.Range(0, 5);
            } while (secondFPtoPlay == firstFPtoPlay);
            do
            {
                thirdFPtoPlay = Random.Range(0, 5);
            } while (thirdFPtoPlay == firstFPtoPlay || thirdFPtoPlay == secondFPtoPlay);
            fireParticles[firstFPtoPlay].maxParticles = fireParticlesMiddleLimit;
            fireParticles[secondFPtoPlay].maxParticles = fireParticlesMiddleLimit;
            fireParticles[thirdFPtoPlay].maxParticles = fireParticlesMiddleLimit;
            if (!fireParticles[secondFPtoPlay].isPlaying)
                fireParticles[secondFPtoPlay].Play();
            if (!fireParticles[thirdFPtoPlay].isPlaying)
                fireParticles[thirdFPtoPlay].Play();
        }
        else if (tankCurHealth < tankHealthFourthIndicator)
        {
            smokeParticles.maxParticles = oneFourthSmokeParticlesLimit * 4;
            if (!smokeParticles.isPlaying)
                smokeParticles.Play();
            do
            {
                fourthFPtoPlay = Random.Range(0, 5);
            } while (fourthFPtoPlay == firstFPtoPlay || fourthFPtoPlay == secondFPtoPlay || 
                fourthFPtoPlay == thirdFPtoPlay);
            do
            {
                fifthFPtoPlay = Random.Range(0, 5);
            } while (fifthFPtoPlay == firstFPtoPlay || fifthFPtoPlay == secondFPtoPlay || 
                fifthFPtoPlay == thirdFPtoPlay || fifthFPtoPlay == fourthFPtoPlay);
            for (int i = 0; i < fireParticles.Length; i++)
            {
                fireParticles[i].maxParticles = fireParticlesMiddleLimit * 2;
                if (!fireParticles[i].isPlaying)
                    fireParticles[i].Play();
            }
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
        } else
        {
            rigidbody_useThis.mass = 15000;
            StartCoroutine(TankDeathExplosion());
        }
    }
}
