using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public static int NumberOfEnemiesThatHaveDied;

    public static event Action EnemyDied;

    void OnMouseDown()
    {
        Die();
    }

	// Use this for initialization
	void Start ()
    {
        EnemyDied = Die;
        EnemyDied += AnotherFunction;
	}

    void Die()
    {
        Debug.Log("The enemy died");
        Destroy(gameObject);

        if (EnemyDied != null)
        {
            EnemyDied.Invoke();
        }

        Destroy(gameObject);
    }
}
