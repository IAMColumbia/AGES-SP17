using UnityEngine;
using System.Collections;
using System;

//public delegate void EnemyDiedDelegate();

public class Enemy : MonoBehaviour
{
    public static event Action OnEnemyDied;

    public static int NumberOfEnemiesThatHaveDied { get; private set; }

    private void OnMouseDown()
    {
        //Debug.Log("Player clicked " + gameObject.name);
        Die();
    }

    private void Die()
    {
        NumberOfEnemiesThatHaveDied++;
        Destroy(gameObject);

        if (OnEnemyDied != null)
        {
            OnEnemyDied.Invoke();
        }
    }
}
