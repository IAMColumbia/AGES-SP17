using UnityEngine;
using System.Collections;
using System;

public class Enemy : MonoBehaviour {

    public static int NumberOfEnemiesThatHaveDied;

    public static event Action EnemyDied;

    void OnMouseDown()
    {
        Die();
    }


    void Die()
    {
        Debug.Log("The enemy died");
        Destroy(gameObject);

        if (EnemyDied != null)
            EnemyDied.Invoke();
        

        Destroy(gameObject);
    }
}
