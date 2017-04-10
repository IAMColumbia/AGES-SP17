﻿using UnityEngine;
using System.Collections;
using System;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int healthValue;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Debug.Log("Health is at " + healthValue.ToString());
	}


    public void TakeDamage(int damageToTake)
    {
        healthValue = healthValue - damageToTake;
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
        gameObject.SetActive(false);
    }
}
