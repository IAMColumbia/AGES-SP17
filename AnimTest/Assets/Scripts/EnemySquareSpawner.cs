using UnityEngine;
using System.Collections;

public class EnemySquareSpawner : MonoBehaviour {

    public GameObject spawntype;

	// Use this for initialization
	void Start ()
    {
        //RELAX! DON'T DO IT!
        InvokeRepeating("SpawnEnemy", 0.0f, 0.6f);
	}
	
	// Update is called once per frame
	void Update ()
    {
        
	}

    void SpawnEnemy()
    {
        GameObject test = (GameObject)Instantiate(Resources.Load("EnemySquare")); ;
    }
}
