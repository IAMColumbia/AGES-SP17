using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StatDisplayUnit : MonoBehaviour {

    [SerializeField]
    Text scoreText;

    [SerializeField]
    Slider scoreSlider;

    float maxScore = 1;
    float playerScore = 0;

    string scorePrefix = "SCO";

    bool scoreIsPercentage = false;

    // Use this for initialization
    void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Init(float max, float player, string prefix = "SCO", bool percent = false)
    {
        maxScore = max;
        scoreSlider.maxValue = maxScore;

        playerScore = player;

        scorePrefix = prefix;

        scoreIsPercentage = percent;

        UpdateProgress(0);
    }

    public void UpdateProgress(float t)
    {
        t = Mathf.Clamp01(t);

        if(t == 1)
        {
            scoreText.text = formatScoreString(playerScore);
            scoreSlider.value = playerScore;
        }
        else
        {
            float score = Mathf.Lerp(0, playerScore, t);

            scoreText.text = formatScoreString(score);
            scoreSlider.value = score;
        }
    }

    string formatScoreString(float score)
    {
        string scorestring = scorePrefix + " ";
        if (scoreIsPercentage)
        {
            scorestring += score.ToString("P1");
        }
        else
        {
            scorestring += Mathf.Floor(score).ToString();
        }
        return scorestring;
    }
}
