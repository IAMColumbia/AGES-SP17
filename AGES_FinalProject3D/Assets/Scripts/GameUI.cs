using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField]
    private Health playerHealth;

    private int score = 0;
    private Text scoreText;
    private Slider healthSlider;


	// Use this for initialization
	void Start ()
    {
        scoreText = GetComponentInChildren<Text>();
        healthSlider = GetComponentInChildren<Slider>();
        healthSlider.maxValue = playerHealth.HealthValue;
	}
	
	// Update is called once per frame
	void Update ()
    {
        healthSlider.value = playerHealth.HealthValue;
	}

    public void UpdateScoreText(int additionalScore)
    {
        score = score + additionalScore;
        scoreText.text = "Score: " + score.ToString();
    }
}
