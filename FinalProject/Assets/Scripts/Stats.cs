using UnityEngine;
using System.Collections;

public class Stats : MonoBehaviour
{
    [SerializeField]
    int maxStrength;
    public int MaxStrength
    {
        get
        {
            return maxStrength;
        }
    }

    [SerializeField]
    int maxArmor;
    public int MaxArmor
    {
        get
        {
            return maxArmor;
        }
    }

    int strength;
    public int Strength
    {
        get
        {
            return strength;
        }
        set
        {
            strength = value;
        }
    }

    int armor;
    public int Armor
    {
        get
        {
            return armor;
        }
        set
        {
            armor = value;
        }
    }

    BattleManager battleManager;

    void Start()
    {
        strength = maxStrength;
        armor = maxArmor;

        battleManager = GameObject.Find("BattleManager").GetComponent<BattleManager>();
    }

    public void TakeStrengthDamage(int amount)
    {
        strength -= amount;

        if (strength <= 0)
        {
            strength = 0;

            Die();
        }
        else
        {
            // play sfx
        }
    }

    public void TakeArmorDamage(int amount)
    {
        armor -= amount;

        if (armor < 0)
        {
            armor = 0;
        }

        // play sfx
    }

    void Die()
    {
        // play sfx

        battleManager.EndBattle();
    }
}
