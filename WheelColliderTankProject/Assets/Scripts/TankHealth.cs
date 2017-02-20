using UnityEngine;
using System.Collections;
using System;

public class TankHealth : MonoBehaviour, IDamagable {

    [SerializeField]
    float currentHealth = 100;
    [SerializeField]
    float maxHealth = 100;

    private int amountOfDamageTaken = 10;

    enum DamageState { NoDamage, Light, Moderate, Heavy, Critical};

    DamageState currentDamageState;


    public void TakeDamage()
    {
        Debug.Log("please");
        currentHealth -= amountOfDamageTaken;
        UpdateDamageState();
    }

    // Use this for initialization
    void Start () {


        currentDamageState = DamageState.NoDamage;

	}

    DamageState UpdateDamageState()
    {
        switch(currentDamageState)
        {
            case DamageState.NoDamage:
                currentDamageState = DamageState.NoDamage;
                break;
            case DamageState.Light:
                currentDamageState = DamageState.Light;
                break;
            case DamageState.Moderate:
                currentDamageState = DamageState.Moderate;
                break;
            case DamageState.Heavy:
                currentDamageState = DamageState.Heavy;
                break;
            case DamageState.Critical:
                currentDamageState = DamageState.Critical;
                Destroy(gameObject);
                break;
       
        }
        return currentDamageState;
    }
}
