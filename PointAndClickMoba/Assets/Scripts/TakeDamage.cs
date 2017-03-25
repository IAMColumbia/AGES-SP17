using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TakeDamage : MonoBehaviour
{
    [SerializeField]
    float maxHealth = 100;

    [HideInInspector]
    public Slider healthSlider;

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
