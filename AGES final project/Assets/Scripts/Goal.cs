using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour
{
    GameManager gameManager;

    // Use this for initialization
    void Start ()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.tag == "Player")
        {
            gameManager.ReachedGoal = true;
        }
    }
}
