using UnityEngine;
using System.Collections;

public class Stats : MonoBehaviour
{
    [SerializeField]
    AudioClip damageAudio;
    [SerializeField]
    AudioClip deathAudio;

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
    AudioSource sfx;
    Animator anim;

    void Start()
    {
        strength = maxStrength;
        armor = maxArmor;

        battleManager = GameObject.Find("BattleManager").GetComponent<BattleManager>();
        sfx = this.GetComponent<AudioSource>();
        anim = this.GetComponent<Animator>();
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
            sfx.clip = damageAudio;
            sfx.PlayDelayed(1.0f);
        }
    }

    public void TakeArmorDamage(int amount)
    {
        armor -= amount;

        if (armor < 0)
        {
            armor = 0;
        }

        sfx.clip = damageAudio;
        sfx.PlayDelayed(1.0f);
    }

    void Die()
    {
        sfx.clip = deathAudio;
        sfx.PlayDelayed(1.0f);
        
        anim.SetTrigger("Die");

        StartCoroutine(battleManager.EndBattle());
    }
}
