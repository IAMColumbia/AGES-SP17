using UnityEngine;
using System.Collections;
using System;

//public delegate void EnemyDiedDelegate();

public class enemy : MonoBehaviour
{
    public static event Action OnEnemyDied;

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
        if(OnEnemyDied != null)
        {
            OnEnemyDied.Invoke();
        }
    }
}
