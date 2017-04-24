using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour
{
    [SerializeField]
    Color activatedColor;
    [SerializeField]
    Transform checkpointMirror;

    GameManager gameManager;
    SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start ()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            spriteRenderer.color = activatedColor;
            gameManager.currentCheckpoint = this.transform;
            gameManager.currentCheckpointMirror = checkpointMirror.transform;
        }
    }
}
