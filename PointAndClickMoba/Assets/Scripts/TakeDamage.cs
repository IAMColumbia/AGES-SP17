using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TakeDamage : MonoBehaviour
{
    [SerializeField]
    Slider healthSlider;
    [SerializeField]
    float maxHealth = 100;

    float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void Damaged(float damage)
    {
        currentHealth -= damage;

        SetHealthUI();

        if (currentHealth <= 0)
        {
            currentHealth = 0;
        }
    }

    void SetHealthUI()
    {
        healthSlider.value = currentHealth;
    }
}
