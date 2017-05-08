using UnityEngine;
using System.Collections;

public class KnightAttacks : MonoBehaviour
{
    Stats knight;
    Stats enemy;

    BattleManager battleManager;

    bool canUseRecklessCharge = true;
    bool canUseBattlePrayer = true;

    void Start()
    {
        knight = this.gameObject.GetComponent<Stats>();
        enemy = GameObject.Find("Necro").GetComponent<Stats>();

        battleManager = GameObject.Find("BattleManager").GetComponent<BattleManager>();
    }

    public void AttackStrength()
    {
        // add animation
        // play sfx

        int amount = knight.Strength - enemy.Armor;

        if (amount < 1)
        {
            amount = 1;
        }

        enemy.TakeStrengthDamage(amount);

        battleManager.EndTurn();
    }

    public void AttackArmor()
    {
        // add animation
        // play sfx

        int amount = knight.Strength;

        enemy.TakeArmorDamage(amount);

        battleManager.EndTurn();
    }

    public void RecklessCharge()
    {
        if (canUseRecklessCharge)
        {
            canUseRecklessCharge = false;

            // add animation
            // play sfx

            int amount = knight.Armor / 3;

            knight.Armor = 0;

            enemy.TakeStrengthDamage(amount);

            battleManager.EndTurn();
        }
        else
        {
            // show not available
        }
    }

    public void BattlePrayer()
    {
        if (canUseBattlePrayer)
        {
            canUseBattlePrayer = false;

            // play sfx

            int amount = knight.MaxArmor - knight.Armor;

            knight.Strength += amount;

            battleManager.EndTurn();
        }
        else
        {
            // show not available
        }
    }
}
