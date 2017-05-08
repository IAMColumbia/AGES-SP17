using UnityEngine;
using System.Collections;

public class NecroAttacks : MonoBehaviour
{
    Stats necro;
    Stats knight;

    void Start()
    {
        necro = this.gameObject.GetComponent<Stats>();
        knight = GameObject.Find("Knight").GetComponent<Stats>();
    }

    public void AttackStrength()
    {
        int amount = necro.Strength - knight.Armor;

        if (amount < 1)
        {
            amount = 1;
        }

        knight.TakeStrengthDamage(amount);
    }

    public void AttackArmor()
    {
        int amount = necro.Strength;

        knight.TakeArmorDamage(amount);
    }

    public void Absorb()
    {
        int amount = necro.Strength - knight.Armor;

        if (amount < 1)
        {
            amount = 1;
        }

        necro.Strength += amount;

        if (necro.Strength > necro.MaxStrength)
        {
            necro.Strength = necro.MaxStrength;
        }

        knight.TakeStrengthDamage(amount);
    }

    public void BoneArmor()
    {
        int amount = necro.MaxStrength - necro.Strength;

        necro.Armor += amount;
    }
}
