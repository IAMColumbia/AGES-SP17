using UnityEngine;
using System.Collections;
using System;

public class PlayerHealth : MonoBehaviour, IDamage
{
    float maxPlayerHealth = 100;

    public float playerHealth;

    PlayerController playerScript;

    [SerializeField]
    ParticleSystem smoke;
    ParticleSystem.EmissionModule smokeEmission;

    [SerializeField]
    ParticleSystem explosion;

    float healthPercentage;

    bool exploding;

    // Initialization
    void Start()
    {
        smokeEmission = smoke.emission;
        playerScript = GetComponent<PlayerController>();
        playerHealth = maxPlayerHealth;
        ResetHealth(); 
    }

    // Update
    void FixedUpdate()
    {
        if (playerHealth < 0 && !exploding)
        {
            exploding = true;
            playerScript.isAlive = false;
            explosion.Play();
            playerScript.ExplosionSound();
            StartCoroutine(playerScript.DeactivateMecha());
        }

        healthPercentage = (playerHealth / maxPlayerHealth)*100;

        smokeEmission.rate = new ParticleSystem.MinMaxCurve(100f - (healthPercentage / .75f));
    }

    public void TakeDamage(float x)
    {
        if (playerScript.isAlive)
        {
            playerHealth = playerHealth - x;

            playerScript.audiosource.clip = playerScript.clip[1];
            playerScript.audiosource.volume = .5f;
            playerScript.audiosource.pitch = UnityEngine.Random.Range(0.1f, 0.5f);
            playerScript.audiosource.Play();
        }
    }

    public void ResetHealth()
    {
        playerHealth = maxPlayerHealth;
        playerScript.isAlive = true;
        exploding = false;
    }
}
