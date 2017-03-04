using UnityEngine;
using System.Collections;
using System;

//public delegate void enemyDieDelegate();

public class Enemy : MonoBehaviour
{
    public static event Action OnEnemyDie;

    public static int NumberOfEnemiesThatHaveDied { get; private set; }

    void OnMouseDown()
    {
        //Debug.Log("Player clicked " + gameObject.name);
        Die();
    }

    private void Die()
    {
        NumberOfEnemiesThatHaveDied++;
        Destroy(gameObject);

        if(OnEnemyDie != null)
            OnEnemyDie.Invoke();
    }
}    