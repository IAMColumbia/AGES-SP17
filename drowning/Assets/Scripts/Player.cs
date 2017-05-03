using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {

    [SerializeField]
    float maxHealth;

    [SerializeField]
    Alarm alarm;

    float health;

    List<IncomingAttack> incomingAttacks = new List<IncomingAttack>();

    bool underAttack = false;

	// Use this for initialization
	void Start () {
        health = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {

        //this block detects edges and prevents the alarm from getting called every update when we're under attack
        if(incomingAttacks.Count > 0 && !underAttack) //if there are incoming attacks but we haven't entered the "under attack" state, turn on the alarm
        {
            underAttack = true;
            StartWarning();
        }
        else if(incomingAttacks.Count == 0 && underAttack) //if all incoming attacks are dealt with but we're still in "under attack" state, turn off the alarm
        {
            underAttack = false;
            StopWarning();
        }

        //increment all incoming attack timers, take damage from any that hit 0
	    for(int i = incomingAttacks.Count - 1; i >= 0; i--)
        {
            IncomingAttack attack = incomingAttacks[i];

            attack.TimeRemaining -= Time.deltaTime;

            if(attack.TimeRemaining <= 0)
            {
                TakeDamage(attack.DamageAmount);
                incomingAttacks.RemoveAt(i);
            }
        }
	}

    void StartWarning()
    {
        alarm.TurnOnAlarm();
    }

    void StopWarning()
    {
        alarm.TurnOffAlarm();
    }

    public void EnemyAttack(float damageAmount, float timeUntilHit)
    {
        incomingAttacks.Add(new IncomingAttack(timeUntilHit, damageAmount));
    }

    void TakeDamage(float amount)
    {
        health -= amount;

        Debug.Log("HIT FOR " + amount + "! REMAINING HEALTH: " + health);
    }
}

public class IncomingAttack
{
    public float TimeRemaining, DamageAmount;

    public IncomingAttack(float timeRemaining, float damageAmount)
    {
        TimeRemaining = timeRemaining;
        DamageAmount = damageAmount;
    }
}
