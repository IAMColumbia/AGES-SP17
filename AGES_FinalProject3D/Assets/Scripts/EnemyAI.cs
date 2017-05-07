using UnityEngine;
using System.Collections;
using System;

//give player rigidbody
[RequireComponent(typeof(Health))]
public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private bool doesMove;

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
        if (doesMove)
        {
            MoveAimlessly();
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        
	}

    private void MoveAimlessly()
    {
        //rigidbody addforce, recognize this means adding a rigidbody to ALL enemies that hope to move aimlessly
    }

    //private void OnTriggerEnter(Collider collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        player = collision.GetComponent<Health>();
    //        if (player != null)
    //        {
    //            player.TakeDamage(1);
    //        }
    //        enemyInstanceHealth.TakeDamage(1);
    //    }
    //}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player = collision.gameObject.GetComponent<Health>();
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
