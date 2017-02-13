using UnityEngine;

public interface IDamageable
{
    float currentHealth { get; set; }
    float maxHealth { get; set; }

    void UpdateDamageState();



}