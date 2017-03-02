using UnityEngine;
using System.Collections;
using System;

public class Enemy : MonoBehaviour {

    public static int NumEnemiesThatHaveDied;

    public static event Action enemyDied;

    void OnMouseDown()
    {
        Die();
    }

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Die()
    {
        Enemy.NumEnemiesThatHaveDied++;

        if (Enemy.enemyDied != null)
        {
            Enemy.enemyDied.Invoke();
        }

        Destroy(this.gameObject);
    }
}
