using UnityEngine;
using System.Collections;

public class Hazard : MonoBehaviour
{
    Player player;

	// Use this for initialization
	void Start ()
    {
        player = GameObject.Find("Player").GetComponent<Player>();	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.tag == "Player")
        {
            player.isAlive = false;
        }
    }
}
