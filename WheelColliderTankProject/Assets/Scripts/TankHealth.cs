using UnityEngine;
using System.Collections;
using System;

public class TankHealth : MonoBehaviour, IDamageable
{
    [SerializeField]
    int maxTankHealth = 100;
    [SerializeField]
    int damageDealt = 33;
    [SerializeField]
    int minorDamageToTank = 67;
    [SerializeField]
    int mediumDamageToTank = 34;
    [SerializeField]
    int majorDamageToTank = 1;
    [SerializeField]
    ParticleSystem minorDamageSteam;
    [SerializeField]
    ParticleSystem mediumDamageSmoke;
    [SerializeField]
    ParticleSystem majorDamageFire;
    [SerializeField]
    ParticleSystem deathFire1;
    [SerializeField]
    ParticleSystem deatheFire2;

    int currentTankHealth;
     
    public void TakeDamage()
    {
        currentTankHealth = currentTankHealth - damageDealt;

        if(currentTankHealth <= minorDamageToTank && currentTankHealth > mediumDamageToTank)
        {
            minorDamageSteam.gameObject.SetActive(true);
            minorDamageSteam.Play();
        }
        else if(currentTankHealth <= mediumDamageToTank && currentTankHealth > majorDamageToTank)
        {
            mediumDamageSmoke.gameObject.SetActive(true);
            mediumDamageSmoke.Play();
        }
        else if(currentTankHealth <= majorDamageToTank && currentTankHealth > 0)
        {
            majorDamageFire.gameObject.SetActive(true);
            majorDamageFire.Play();
        }
        else if(currentTankHealth <= 0)
        {
            deathFire1.gameObject.SetActive(true);
            deathFire1.Play();
            deatheFire2.gameObject.SetActive(true);
            deatheFire2.Play();
            this.gameObject.GetComponent<TankController>().enabled = false;
        }

        Debug.Log("Current Health is " + currentTankHealth);
    }

    // Use this for initialization
    void Start ()
    {
        currentTankHealth = maxTankHealth;
        //this.gameObject.GetComponent<TankController>();
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
