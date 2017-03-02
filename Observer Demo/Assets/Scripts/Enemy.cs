using UnityEngine;
using System.Collections;
using System;

public class Enemy : MonoBehaviour {

    // Use this for initialization
    public static int NumberOfEnemiesThatHaveDied;

   public static event Action EnemyDied;

    void OnMouseDown()
    {
        Die();
    }

    void Die()
    {
        Debug.Log("The enemy  Died");

        NumberOfEnemiesThatHaveDied++;

        if (EnemyDied != null)
        {
            EnemyDied.Invoke();
        }
        Destroy(gameObject);
    }
  
   
	
	
	
}
