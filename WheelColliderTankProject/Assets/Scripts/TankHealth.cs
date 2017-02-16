using UnityEngine;
using System.Collections;
using System;

interface IDamageable
{
    void TakeDamage(float amount);
}

public class TankHealth : MonoBehaviour, IDamageable {

    [SerializeField]
    float MaxHealth = 2000;

    public float CurrentHealth { get; private set; }

    public bool Alive { get; private set; }

    // Use this for initialization
    void Start () {
        CurrentHealth = MaxHealth;
        Alive = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Die()
    {
        Alive = false;
    }

    public void TakeDamage(float amount)
    {
        CurrentHealth -= amount;

        if(CurrentHealth <= 0 && Alive)
        {
            Die();
        }
    }
}
