using UnityEngine;
using System.Collections;

public class KnightAttacks : MonoBehaviour
{
    [SerializeField]
    AudioClip attackAudio;

    Stats knight;
    Stats enemy;

    BattleManager battleManager;
    AudioSource sfx;
    Animator anim;

    bool canUseRecklessCharge = true;
    bool canUseBattlePrayer = true;

    void Start()
    {
        knight = this.gameObject.GetComponent<Stats>();
        enemy = GameObject.Find("Necro").GetComponent<Stats>();

        battleManager = GameObject.Find("BattleManager").GetComponent<BattleManager>();
        sfx = this.GetComponent<AudioSource>();
        anim = this.GetComponent<Animator>();
    }

    public void AttackStrength()
    {
        anim.SetTrigger("Attack");
        sfx.clip = attackAudio;
        sfx.PlayDelayed(0.5f);

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
        anim.SetTrigger("Attack");
        sfx.clip = attackAudio;
        sfx.PlayDelayed(0.5f);

        int amount = knight.Strength;

        enemy.TakeArmorDamage(amount);

        battleManager.EndTurn();
    }

    public void RecklessCharge()
    {
        if (canUseRecklessCharge)
        {
            canUseRecklessCharge = false;
            
            anim.SetTrigger("Attack");
            sfx.clip = attackAudio;
            sfx.PlayDelayed(0.5f);

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

            sfx.clip = attackAudio;
            sfx.PlayDelayed(0.5f);

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
