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

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            gameManager.ReachedGoal = true;
        }
    }
}
