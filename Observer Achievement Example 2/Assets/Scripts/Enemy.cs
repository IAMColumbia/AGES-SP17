using UnityEngine;
using System.Collections;
using System;

public class Enemy : MonoBehaviour
{
    public static event Action EnemyDied;
    public static int NumberKilled { get; private set; }

    private void OnMouseDown()
    {
        Die();
    }

    private void Die()
    {
        NumberKilled++;
        Debug.Log("Enemy Died! NumberKilled: " + NumberKilled);

        if (EnemyDied != null)
            EnemyDied.Invoke();

        Destroy(gameObject);
    }

    
}
