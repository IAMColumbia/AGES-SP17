using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TakeDamage : MonoBehaviour
{
    [SerializeField]
    float maxHealth = 100;
    [SerializeField]
    ParticleSystem explosion;

    [HideInInspector]
    public Slider healthSlider;
    public GameObject Cannon;
    public GameObject HealthBar;

    float currentHealth;
    EndGame endGame;

    void Start()
    {
        endGame = GameObject.Find("EndGame").GetComponent<EndGame>();
        currentHealth = maxHealth;
    }

    public void Damaged(float damage)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damage;

            SetHealthUI();

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                Explode();
            }
        }
    }

    void SetHealthUI()
    {
        healthSlider.value = currentHealth;
    }

    void Explode()
    {
        GetComponent<PlayerMovement>().enabled = false;
        Instantiate(explosion, transform.position, transform.rotation, transform);
        endGame.OnDeath();
        Destroy(Cannon, explosion.duration);
        Destroy(HealthBar, explosion.duration);
        Destroy(gameObject, explosion.duration);
    }
}
