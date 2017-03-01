using UnityEngine;
using System.Collections;
using System;

public class Enemy : MonoBehaviour {

    public static int numberOfEnemiesKilled;

    public static event Action EnemyDied;

    void OnMouseDown()
    {
        Die();
    }

    void Die()
    {
        Debug.Log("The Enemy Died!");

        numberOfEnemiesKilled++;

        if (EnemyDied != null)
        {
            EnemyDied.Invoke();
        }

        Destroy(gameObject);
    }
}
