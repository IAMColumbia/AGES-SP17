using UnityEngine;
using System.Collections;
using System;

public class BuildingManager : MonoBehaviour {

    public ParticleSystem explosionSystem;
    public AudioSource explosionAudio;
    public AudioClip glassBreak;


    int stateOfBuilding;
    int buildingHealth = 15;

    private void Start()
    {

        stateOfBuilding = UnityEngine.Random.Range(1, 5);

        if(stateOfBuilding == 1)
        {
            gameObject.layer = LayerMask.NameToLayer("PassThroughBuilding");
        }
        else if(stateOfBuilding == 2)
        {
            gameObject.layer = LayerMask.NameToLayer("PassThroughShell");
        }
        else if(stateOfBuilding == 3)
        {
            gameObject.layer = LayerMask.NameToLayer("PassThroughTank");
        }
        else if(stateOfBuilding == 4)
        {
            gameObject.layer = LayerMask.NameToLayer("Damagable");
        }
    }

    public void TakeDamage()
    {
        Debug.Log("building health: " + buildingHealth);
        buildingHealth--;


        AudioSource buildingDamage = GetComponent<AudioSource>();
        buildingDamage.clip = glassBreak;
        buildingDamage.Play();

        if(buildingHealth <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        explosionAudio.transform.position = transform.position;
        explosionSystem.gameObject.SetActive(true);

        explosionSystem.Play();

        explosionAudio.Play();

        gameObject.SetActive(false);
    }
}
