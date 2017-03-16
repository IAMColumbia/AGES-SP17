using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ScoreText : MonoBehaviour {

    [SerializeField]
    PlayerController player;

    Text scoreText;

	// Use this for initialization
	void Start () {
        scoreText = GetComponent<Text>();	
	}

    // Update is called once per frame
    void Update() {
        scoreText.text = player.Score.ToString() + " pts.";
	}
}
