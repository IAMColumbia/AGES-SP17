using UnityEngine;
using System.Collections;
using System;

public delegate void EnemyDiedDelegate();

public class Enemy : MonoBehaviour {

    public static event Action OnEnemyDied;

    public static int NumberOfEnemiesThatHaveDied { get; private set; }

    void OnMouseDown()
    {
        Die();
    }

    private void Die()
    {
        Destroy(gameObject);

        if (OnEnemyDied != null)
            OnEnemyDied.Invoke();
    }
}
