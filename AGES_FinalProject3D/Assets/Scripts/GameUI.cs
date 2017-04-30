using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    [SerializeField]
    private Health playerHealth;
    [SerializeField]
    private GameObject AfterActionReport;
    [SerializeField]
    private Text finalScoreText;

    private int score = 0;
    private Text scoreText;
    private Text winOrLoseText;
    private Slider healthSlider;

	// Use this for initialization
	void Start ()
    {
        scoreText = GetComponentInChildren<Text>();

        winOrLoseText = AfterActionReport.GetComponentInChildren<Text>();

        winOrLoseText.text = "";

        AfterActionReport.SetActive(false);
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
        scoreText.text = "Score:" + score.ToString();
    }

    public void Win()
    {
        AfterActionReport.SetActive(true);
        winOrLoseText.text = "You Win!";
        finalScoreText.text += score.ToString();
    }

    public void Lose()
    {
        AfterActionReport.SetActive(true);
        winOrLoseText.text = "You Lose!";
        finalScoreText.text += score.ToString();
    }

    #region buttonFunctions

    public void LoadScene(string sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void Quit()
    {
        Application.Quit();
    }
    #endregion
}
