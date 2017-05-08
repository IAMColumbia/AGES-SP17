using UnityEngine;
using System.Collections;

public class KnightAttacks : MonoBehaviour
{
    Stats knight;
    Stats enemy;

    void Start()
    {
        knight = this.gameObject.GetComponent<Stats>();
        enemy = GameObject.Find("Necro").GetComponent<Stats>();
    }

    public void AttackStrength()
    {
        int amount = knight.Strength - enemy.Armor;

        if (amount < 1)
        {
            amount = 1;
        }

        enemy.TakeStrengthDamage(amount);
    }

    public void AttackArmor()
    {
        int amount = knight.Strength;

        enemy.TakeArmorDamage(amount);
    }

    public void ShedArmor()
    {
        int amount = knight.Armor / 3;

        knight.Armor = 0;

        enemy.TakeStrengthDamage(amount);
    }

    public void Meditate()
    {
        int amount = knight.MaxArmor - knight.Armor;

        knight.Strength += amount;
    }
}
