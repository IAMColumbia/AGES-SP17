using UnityEngine;
using System.Collections;

public class NecroAttacks : MonoBehaviour
{
    Stats necro;
    Stats knight;

    BattleManager battleManager;

    bool canUseAbsorb = true;
    bool canUseBoneArmor = true;

    void Start()
    {
        necro = this.gameObject.GetComponent<Stats>();
        knight = GameObject.Find("Knight").GetComponent<Stats>();

        battleManager = GameObject.Find("BattleManager").GetComponent<BattleManager>();
    }

    void AttackStrength()
    {
        // add animation
        // play sfx

        int amount = necro.Strength - knight.Armor;

        if (amount < 1)
        {
            amount = 1;
        }

        knight.TakeStrengthDamage(amount);

        battleManager.EndTurn();
    }

    void AttackArmor()
    {
        // add animation
        // play sfx

        int amount = necro.Strength;

        knight.TakeArmorDamage(amount);

        battleManager.EndTurn();
    }

    void Absorb()
    {
        canUseAbsorb = false;

        // add animation
        // play sfx

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

        battleManager.EndTurn();
    }

    void BoneArmor()
    {
        canUseBoneArmor = false;

        //play sfx

        int amount = necro.MaxStrength - necro.Strength;

        necro.Armor += amount;

        battleManager.EndTurn();
    }

    public void ChooseAttack()
    {
        if (necro.Strength <= 5 && canUseBoneArmor)
        {
            BoneArmor();
        }
        else if (necro.Strength < knight.Armor)
        {
            AttackArmor();
        }
        else if (canUseAbsorb)
        {
            Absorb();
        }
        else
        {
            AttackStrength();
        }
    }
}
