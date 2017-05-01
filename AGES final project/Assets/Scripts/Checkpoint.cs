using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour
{
    Animator animator;
    GameManager gameManager;

	// Use this for initialization
	void Start ()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        animator = gameObject.GetComponentInChildren<Animator>();
    }
	
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            animator.SetBool("IsActivated", true);
            gameManager.currentCheckpoint = this.transform;
        }
    }
}
