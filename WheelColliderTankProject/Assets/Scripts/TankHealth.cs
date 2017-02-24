using UnityEngine;
using System.Collections;


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
    [SerializeField]
    GameObject tankTurret;

    int currentTankHealth;
    TankController tankController;
     
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
            StartCoroutine(BlowUpTwice());
            tankController.enabled = false;
            this.gameObject.GetComponent<TankShooting>().enabled = false;
            tankTurret.gameObject.GetComponent<TankTurret>().enabled = false;
        }

        Debug.Log("Current Health is " + currentTankHealth);
    }

    private IEnumerator BlowUpTwice()
    {
        yield return new WaitForSeconds(1f);
        tankController.Explode(gameObject.transform.position);
        yield return new WaitForSeconds(3f);
        tankController.Explode(gameObject.transform.position);

    }

    // Use this for initialization
    void Start ()
    {
        tankController = gameObject.GetComponent<TankController>();
        currentTankHealth = maxTankHealth;
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
