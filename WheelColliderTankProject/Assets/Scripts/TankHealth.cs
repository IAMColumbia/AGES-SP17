using UnityEngine;
using System.Collections;
using System;

public class TankHealth : MonoBehaviour, IDamageable
{

    [SerializeField] float currentHealth;
    [SerializeField] float maxHealth;

    public enum DamageState { Light, Medium, Heavy, Critical };

    private DamageState curDamageState;

	void Start ()
    {
	
	}
	
	void Update ()
    {
        UpdateDamageState();
	}

    private void UpdateDamageState()
    {
        switch (curDamageState)
        {
            case DamageState.Light:
                break;
            case DamageState.Medium:
                break;
            case DamageState.Heavy:
                break;
            case DamageState.Critical:
                break;
            default:
                break;
        }
    }

    public void TakeDamage()
    {

    }
}
