using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class KnightAttacks : MonoBehaviour
{
    [SerializeField]
    AudioClip attackAudio;
    [SerializeField]
    Text attackText;

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

        StartCoroutine(ShowAttackText("Attack Strength"));

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

        StartCoroutine(ShowAttackText("Attack Armor"));

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

            StartCoroutine(ShowAttackText("Reckless Charge"));

            int amount = knight.Armor / 3;

            knight.Armor = 0;

            enemy.TakeStrengthDamage(amount);

            battleManager.EndTurn();
        }
    }

    public void BattlePrayer()
    {
        if (canUseBattlePrayer)
        {
            canUseBattlePrayer = false;

            sfx.clip = attackAudio;
            sfx.PlayDelayed(0.5f);

            StartCoroutine(ShowAttackText("Battle Prayer"));

            int amount = knight.MaxArmor - knight.Armor;

            knight.Strength += amount;

            battleManager.EndTurn();
        }
    }

    public void ChangeTextColor(Button button)
    {
        Text text;
        text = button.GetComponentInChildren<Text>();
        text.color = Color.gray;
    }

    IEnumerator ShowAttackText(string text)
    {
        attackText.text = text;

        yield return new WaitForSeconds(2.0f);

        attackText.text = "";
    }
}
