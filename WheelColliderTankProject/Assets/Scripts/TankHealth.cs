using UnityEngine;
using System.Collections;
using System;

public class TankHealth : MonoBehaviour, IHeavyExplodableObject, IDamageable
{
    [SerializeField]
    private ParticleSystem lightDamageParticles;
    [SerializeField]
    private ParticleSystem mediumDamageParticles;
    [SerializeField]
    private ParticleSystem heavyDamageParticles;
    [SerializeField]
    private ParticleSystem criticalDamageParticles;

    private int maxHealth;
    private int currentHealth;
    public int CurrentHealth
    {
        get
        {
            return currentHealth;
        }
    }

    enum DamageState {None = 0, Light = 10, Medium = 50, Heavy = 75, Critical = 100};
    DamageState currentDamage;
    // Use this for initialization
    void Start ()
    {
        maxHealth = 100;
        currentHealth = maxHealth;
        currentDamage = DamageState.None;
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    private void UpdateDamageState()
    {
        switch (currentDamage)
        {
            case DamageState.None:
                break;
            case DamageState.Light:
                lightDamageParticles.gameObject.SetActive(true);
                lightDamageParticles.Play();
                break;
            case DamageState.Medium:
                lightDamageParticles.gameObject.SetActive(false);
                mediumDamageParticles.gameObject.SetActive(true);
                mediumDamageParticles.Play();
                break;
            case DamageState.Heavy:
                mediumDamageParticles.gameObject.SetActive(false);
                heavyDamageParticles.gameObject.SetActive(true);
                heavyDamageParticles.Play();
                break;
            case DamageState.Critical:
                DestroyTank();
                break;
            default:
                break;
        }
    }

    private IEnumerator DestroyTank()
    {
        criticalDamageParticles.Play();
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    public void TakeDamage()
    {
        currentHealth -= 33;

        int totalDamage = maxHealth - currentHealth;

        if (totalDamage == 0)
        {
            currentDamage = DamageState.None;
        }

        if (totalDamage == 10)
        {
            currentDamage = DamageState.Light;
        }

        if (totalDamage == 50)
        {
            currentDamage = DamageState.Medium;
        }

        if (totalDamage == 75)
        {
            currentDamage = DamageState.Heavy;
        }

        if (totalDamage == 100)
        {
            currentDamage = DamageState.Critical;
        }

        UpdateDamageState();

        Debug.Log("Health is at " + currentHealth.ToString());
    }

    public void Explode(Vector3 incomingProjectileDirection)
    {

    }
}
