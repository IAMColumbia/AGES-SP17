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

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            gameUI.UpdateScoreText(scoreValue);
            gameObject.SetActive(false);
        }
    }
}
