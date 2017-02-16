using UnityEngine;
using System.Collections;
using System;

public class TankHealth : MonoBehaviour, IDamagable
{
    [SerializeField]
    float maxHealth = 50;

    float health;
    float healthPercentage;

    public void Start()
    {
        health = maxHealth;
        healthPercentage = health / maxHealth;
    }

    public void Update()
    {
        if (healthPercentage >= .1 && <= .5)
        {
            //light smoke
        }
        else if (healthPercentage > .5 && <= .74)
        {
            //denser smoke
        }
        else if (healthPercentage > .74 && <= .99)
        {
            //small fire
        }
        else if (healthPercentage > .99)
        {
            //big fire
        }
    }

    public void TakeDamage()
    {
        //do damage
    }
}
