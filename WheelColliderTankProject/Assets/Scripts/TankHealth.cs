using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class TankHealth : MonoBehaviour, IDamagable
{
    [SerializeField]
    ParticleSystem lightSmokeParticles;

    [SerializeField]
    ParticleSystem denseSmokeParticles;

    [SerializeField]
    ParticleSystem smallFireParticles;

    [SerializeField]
    ParticleSystem bigFireParticles;

    bool lightSmokeParticlesIsPlaying;
    bool denseSmokeParticlesIsPlaying;
    bool smallFireParticlesIsPlaying;
    bool bigFireParticlesIsPlaying;

    [SerializeField]
    float maxHealth = 50f;

    float health;
    float healthPercentage;

    public void Start()
    {
        health = maxHealth;
        healthPercentage = health / maxHealth;

        lightSmokeParticles.gameObject.SetActive(false);
        denseSmokeParticles.gameObject.SetActive(false);
        smallFireParticles.gameObject.SetActive(false);
        bigFireParticles.gameObject.SetActive(false);

        lightSmokeParticlesIsPlaying = false;
        denseSmokeParticlesIsPlaying = false;
        smallFireParticlesIsPlaying = false;
        bigFireParticlesIsPlaying = false;
    }
    
    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        healthPercentage = health / maxHealth;

        UpdateDamageState();
    }

    private void UpdateDamageState()
    {
        if (healthPercentage <= .9f && !lightSmokeParticlesIsPlaying)
        {
            lightSmokeParticles.gameObject.SetActive(true);
            lightSmokeParticles.Play();
            lightSmokeParticlesIsPlaying = true;
        }

        if (healthPercentage < .5f && !denseSmokeParticlesIsPlaying)
        {
            denseSmokeParticles.gameObject.SetActive(true);
            denseSmokeParticles.Play();
            denseSmokeParticlesIsPlaying = true;
        }

        if (healthPercentage < .36f && !smallFireParticlesIsPlaying)
        {
            smallFireParticles.gameObject.SetActive(true);
            smallFireParticles.Play();
            smallFireParticlesIsPlaying = true;
        }

        if (healthPercentage < .01f && !bigFireParticlesIsPlaying)
        {
            bigFireParticles.gameObject.SetActive(true);
            bigFireParticles.Play();
            bigFireParticlesIsPlaying = true;
        }

        if (healthPercentage <= 0f)
        {
            GetComponent<TankController>().canControl = false;
            GetComponent<TankShooting>().canShoot = false;
            gameObject.GetComponentInChildren<TankTurret>().canControl = false;

            StartCoroutine(EndGame());
        }
    }

    IEnumerator EndGame()
    {
        yield return new WaitForSeconds(5);

        SceneManager.LoadScene("MainMenu");
    }
}
