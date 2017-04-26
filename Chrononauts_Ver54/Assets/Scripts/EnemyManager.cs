using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {

    //Conceptualize how this is going to work
    //Fatass array of enemies
    //On a certain time = spawn X

    // Use this for initialization

    [SerializeField]
    public GameObject[] objectsToSpawn;

	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void SpawnAll()
    {
        //Go through every object and spawn it. 
        //AnimationIsPlaying = set to true
        //
    }

    
}
