using UnityEngine;
using System.Collections;

public class FireControlVertebraeEnemy : MonoBehaviour {

    Enemy enemy;

    [SerializeField]
    Transform[] firingPositionsParallel, firingPositionsAngled;

	// Use this for initialization
	void Start () {
        enemy = GetComponent<Enemy>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ShootParallel()
    {
        foreach (Transform t in firingPositionsParallel)
        {
            enemy.Shoot(t);
        }
    }

    public void ShootAngled()
    {
        foreach (Transform t in firingPositionsAngled)
        {
            enemy.Shoot(t);
        }
    }
}
