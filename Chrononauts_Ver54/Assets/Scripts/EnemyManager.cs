using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {

    //Conceptualize how this is going to work
    //Fatass array of enemies
    //On a certain time = spawn X

    // Use this for initialization

    int objectSpawnEnum = 0;
    int spawnID = 0;

    [SerializeField]
    public GameObject[] objectsToSpawn;

    [SerializeField]
    public float[] spawnTimes;

	void Start ()
    {
        SpawnEnemies();
    }
	
	// Update is called once per frame
	void Update ()
    {
        
	}

    void SpawnEnemies()
    {
        for (int i = 0; i < objectsToSpawn.Length; i++)
        {
            StartCoroutine(SpawnEnemy(objectsToSpawn[i], spawnTimes[i]));
        }
    }

    private IEnumerator SpawnEnemy(GameObject enemy, float spawnTime)
    {
        yield return new WaitForSeconds(spawnTime);
        enemy.gameObject.SetActive(true);
    }
}
