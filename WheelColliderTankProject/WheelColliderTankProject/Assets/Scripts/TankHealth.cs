﻿using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class TankHealth : MonoBehaviour, IDamageable
{
    public int startingHealth;
    int currentHealth;
    enum DamageState { Undamaged, Light, Medium, Heavy, Critical };
    DamageState tankDamageState;
    [SerializeField] List<DamageParticalEffect> particleEffects;

    [Serializable]
    struct DamageParticalEffect
    {
        /// <summary>
        /// The three percentages at which the VFX will be shown (0, 100).
        /// Numbers should be descending.
        /// </summary>
        public int healthPercentageThreshhold;
        public GameObject particleEffect;
    }

    void Start()
    {
        currentHealth = startingHealth;
        tankDamageState = DamageState.Undamaged;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UpdateDamageState();

    }

    void UpdateDamageState()
    {
        if (currentHealth <= startingHealth * (particleEffects[0].healthPercentageThreshhold * 0.01))
        {
            //play light smoke
            particleEffects[0].particleEffect.SetActive(true);
            tankDamageState = DamageState.Light;
        }

        if (currentHealth <= startingHealth * (particleEffects[1].healthPercentageThreshhold * 0.01))
        {
            //play heavy smoke
            particleEffects[1].particleEffect.SetActive(true);
            tankDamageState = DamageState.Medium;
        }

        if (currentHealth <= startingHealth * (particleEffects[2].healthPercentageThreshhold * 0.01))
        {
            //play small fire
            particleEffects[2].particleEffect.SetActive(true);
            tankDamageState = DamageState.Heavy;
        }

        if (currentHealth <= (particleEffects[3].healthPercentageThreshhold * 0.01))
        {
            //play larget fire
            particleEffects[3].particleEffect.SetActive(true);
            tankDamageState = DamageState.Critical;

            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        Rigidbody myRigidbody = gameObject.GetComponent<Rigidbody>();
        myRigidbody.mass = 100;

        myRigidbody.velocity = Vector3.zero;

        myRigidbody.AddExplosionForce(1f, transform.position - new Vector3(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value), 5f);
        myRigidbody.angularDrag = 0;

        yield return new WaitForSeconds(2);

        myRigidbody.velocity = Vector3.zero;
        myRigidbody.AddExplosionForce(1f, transform.position - new Vector3(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value), 5f);
    }
}
