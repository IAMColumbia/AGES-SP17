using UnityEngine;
using System.Collections;

public class TankHealth : MonoBehaviour, IDamagable {

    //Private Variables
    private int startingHealth = 100;
    private int currentHealth = 100;
    private DamageState damageState = 0;

    //Serialized Fields
    [SerializeField]
    public ParticleSystem[] damageStateParticles;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
        //check for impact from shell, if yes, run take damage to reduce health
	}

    public void TakeDamage()
    {
        currentHealth -= 5;

        if(currentHealth >= 90)
        {
            damageState.Equals(0);
        }
        else if(currentHealth < 90 && currentHealth >= 50)
        {
            damageState.Equals(0);
        }
        else if (currentHealth < 50 && currentHealth >= 30)
        {
            damageState.Equals(0);
        }
        else if (currentHealth < 30 && currentHealth >= 5)
        {
            damageState.Equals(3);
        }
        else if (currentHealth < 5)
        {
            damageState.Equals(4);
        }
        else
        {
            damageState.Equals(5);
        }

        UpdateDamageState();
    }

    public void UpdateDamageState()
    {
        //switch statement for particle effects
        switch (damageState) {

        }
    }
}

enum DamageState {NoDamage, LightDamage, MediumDamage, HeavyDamage, CriticalDamage, Destroyed};
