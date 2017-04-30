using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int healthValue;

    [SerializeField]
    private ParticleSystem deathExplosion;

    private AudioSource deathSound;

    public int HealthValue
    {
        get
        {
            return healthValue;
        }
    }

	// Use this for initialization
	void Start ()
    {
        deathSound = deathExplosion.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Debug.Log("Health is at " + healthValue.ToString());
	}


    public void TakeDamage(int damageToTake)
    {
        float xRandom = Random.Range(2.5f, 5);
        float yRandom = Random.Range(2.5f, 5);
        float zRandom = Random.Range(2.5f, 5);
        healthValue = healthValue - damageToTake;
        gameObject.transform.Rotate(xRandom, yRandom, zRandom);
        DieWhenHealthIsAtZero();
    }

    private void DieWhenHealthIsAtZero()
    {
        if (healthValue <= 0)
        {
            Die();
        }
    }

    //Used for what sequence of events happens when player "Dies"
    private void Die()
    {
        deathExplosion.gameObject.transform.position = gameObject.transform.position;
        deathExplosion.Play();
        deathSound.Play();
        gameObject.SetActive(false);
    }
}
