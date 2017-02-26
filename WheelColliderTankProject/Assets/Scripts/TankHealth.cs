using UnityEngine;
using System.Collections;
using System;

public class TankHealth : MonoBehaviour, IDamagable {

    [SerializeField]
    private float maxHealth = 100;

    public ParticleSystem[] damageParticleSystem;

    public GameObject[] damageParticleGameObject;

    private float currentHealth;
    private int amountOfDamageTaken = 5;

    public enum DamageState {NoDamage,Light, Moderate, Heavy, Critical};

    public DamageState currentDamageState;

    private void Awake()
    {
        foreach (GameObject item in damageParticleGameObject)
        {
            for (int i = 0; i < damageParticleSystem.Length; i++)
            {
                damageParticleSystem[i] = Instantiate(damageParticleGameObject[i]).GetComponent<ParticleSystem>();
                damageParticleSystem[i].gameObject.SetActive(false);
            }
        }
        
        currentDamageState = DamageState.NoDamage;

        currentHealth = maxHealth;
    }

    public void TakeDamage()
    {

        currentHealth -= amountOfDamageTaken;
        
        Debug.Log(currentHealth);
        
        UpdateDamageState();
    }

    private void UpdateDamageState()
    {
        switch(currentDamageState)
        {
            case DamageState.NoDamage:

                if (currentHealth > 75)
                    currentDamageState = DamageState.NoDamage;
                else
                    currentDamageState = DamageState.Light;

                Debug.Log(currentDamageState);

                break;

            case DamageState.Light:

                if (currentHealth > 50)
                {
                    currentDamageState = DamageState.Light;

                    damageParticleSystem[0].gameObject.SetActive(true);

                    damageParticleSystem[0].transform.position = transform.position;

                    damageParticleSystem[0].Play();
                }
                else
                    currentDamageState = DamageState.Moderate;

                Debug.Log(currentDamageState);

                break;

            case DamageState.Moderate:

                if (currentHealth > 25)
                {
                    currentDamageState = DamageState.Moderate;

                    damageParticleSystem[1].gameObject.SetActive(true);

                    damageParticleSystem[1].transform.position = transform.position;

                    damageParticleSystem[1].Play();
                }
                else
                    currentDamageState = DamageState.Heavy;

                Debug.Log(currentDamageState);

                break;

            case DamageState.Heavy:

                if (currentHealth > 5)
                {
                    currentDamageState = DamageState.Heavy;

                    damageParticleSystem[2].gameObject.SetActive(true);

                    damageParticleSystem[2].transform.position = transform.position;

                    damageParticleSystem[2].Play();
                }
                else
                    currentDamageState = DamageState.Critical;

                Debug.Log(currentDamageState);

                break;

            case DamageState.Critical:

                currentDamageState = DamageState.Critical;

                if(currentHealth <= 0)
                {
                    damageParticleSystem[3].gameObject.SetActive(true);

                    damageParticleSystem[3].transform.position = transform.position;

                    damageParticleSystem[3].Play();

                    for (int i = 0; i < damageParticleSystem.Length; i++)
                    {
                        Destroy(damageParticleSystem[i]);
                    }

                    Destroy(gameObject);
                }
                Debug.Log(currentDamageState);

                break;
       
        }
    }
}
