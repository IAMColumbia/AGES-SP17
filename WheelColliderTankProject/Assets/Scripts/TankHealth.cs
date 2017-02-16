using UnityEngine;
using System.Collections;
using System;

public class TankHealth : MonoBehaviour, IDamagable {
    public Enum DamageStatus;

    [SerializeField]
    float maxHealth;
    float currentHealth;

    public void DoDamage(int damageAmount) {
        currentHealth -= damageAmount;
    }
    
}
