using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Health))]
public class EnemyAI : MonoBehaviour
{

    private Health enemyInstanceHealth;
    private Health player;

    private GameUI gameUI;

    [SerializeField]
    private int scoreValue = 10;

	// Use this for initialization
	void Start ()
    {
        enemyInstanceHealth = GetComponent<Health>();
        gameUI = GameObject.Find("GameUI").GetComponent<GameUI>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    private void OnTriggerEnter(Collider collision)
    {
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

    private void OnDisable()
    {
        if (player == null)
        {
            gameUI.UpdateScoreText(scoreValue);
        }
    }
}
