using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Health))]
public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private GameObject deathExplosion;

    private Health enemyInstanceHealth;

	// Use this for initialization
	void Start ()
    {
        enemyInstanceHealth = GetComponent<Health>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    private void OnTriggerEnter(Collider collision)
    {
        Health player;
        if (collision.gameObject.tag == "Player")
        {
            player = collision.GetComponent<Health>();
            if (player != null)
            {
                player.TakeDamage(1);
            }

            enemyInstanceHealth.TakeDamage(1);
        }
    }
}
