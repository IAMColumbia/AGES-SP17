using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NecroAttacks : MonoBehaviour
{
    [SerializeField]
    AudioClip attackAudio;
    [SerializeField]
    Text attackText;

    Stats necro;
    Stats knight;

    BattleManager battleManager;
    AudioSource sfx;
    Animator anim;

    bool canUseAbsorb = true;
    bool canUseBoneArmor = true;

    void Start()
    {
        necro = this.gameObject.GetComponent<Stats>();
        knight = GameObject.Find("Knight").GetComponent<Stats>();

        battleManager = GameObject.Find("BattleManager").GetComponent<BattleManager>();
        sfx = this.GetComponent<AudioSource>();
        anim = this.GetComponent<Animator>();
    }

    void AttackStrength()
    {
        anim.SetTrigger("Attack");
        sfx.clip = attackAudio;
        sfx.PlayDelayed(0.5f);

        StartCoroutine(ShowAttackText("Attack Strength"));

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
        anim.SetTrigger("Attack");
        sfx.clip = attackAudio;
        sfx.PlayDelayed(0.5f);

        StartCoroutine(ShowAttackText("Attack Armor"));

        int amount = necro.Strength;

        knight.TakeArmorDamage(amount);

        battleManager.EndTurn();
    }

    void Absorb()
    {
        canUseAbsorb = false;
        
        anim.SetTrigger("Attack");
        sfx.clip = attackAudio;
        sfx.PlayDelayed(0.5f);

        StartCoroutine(ShowAttackText("Lifesteal"));

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

        sfx.clip = attackAudio;
        sfx.PlayDelayed(0.5f);

        StartCoroutine(ShowAttackText("Bone Armor"));

        int amount = necro.MaxStrength - necro.Strength;

        necro.Armor += amount;

        battleManager.EndTurn();
    }

    public void ChooseAttack()
    {
        StartCoroutine(MoveAI());
    }

    IEnumerator MoveAI()
    {
        yield return new WaitForSeconds(1.0f);

        if (necro.Strength > 0)
        {
            if (necro.Strength <= 5 && necro.Strength < knight.Strength && canUseBoneArmor)
            {
                BoneArmor();
            }

            else if ((necro.Strength - knight.Armor < knight.Strength / 2) && (necro.Strength / 2 <= knight.Armor))
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

    IEnumerator ShowAttackText(string text)
    {
        attackText.text = text;

        yield return new WaitForSeconds(2.0f);

        attackText.text = "";
    }
}
