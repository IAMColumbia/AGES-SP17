using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    public static int NumberOfEnemiesKilled;

    public static event Action EnemyDied;

    void Die()
    {
        Debug.Log("The enemy died!");

        NumberOfEnemiesKilled++;

        if (EnemyDied != null)
        {
            EnemyDied.Invoke();
        }

        Destroy(gameObject);
    }

    void OnMouseDown()
    {
        Die();
    }
}
