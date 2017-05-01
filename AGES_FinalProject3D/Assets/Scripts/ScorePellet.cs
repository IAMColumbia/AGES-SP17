using UnityEngine;
using System.Collections;

public class ScorePellet : MonoBehaviour
{
    private GameUI gameUI;

    private const int scoreValue = 100;
	// Use this for initialization
	void Start ()
    {
        gameUI = GameObject.Find("GameUI").GetComponent<GameUI>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            gameUI.UpdateScoreText(scoreValue);
            gameObject.SetActive(false);
        }
    }
}
