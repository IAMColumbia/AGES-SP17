using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameUI : MonoBehaviour
{
    [SerializeField]
    private Health playerHealth;
    [SerializeField]
    private GameObject afterActionReport;
    [SerializeField]
    private GameObject pauseScreen;
    [SerializeField]
    private Text finalScoreText;
    [SerializeField]
    private string pauseButton;
    [SerializeField]
    private GameObject firstSelectedButton;

    private int score = 0;
    private bool isPaused = false;
    private Text scoreText;
    private Text winOrLoseText;
    private Slider healthSlider;
    private EventSystem gameEventSystem;
    // Use this for initialization
    void Start ()
    {
        scoreText = GetComponentInChildren<Text>();

        winOrLoseText = afterActionReport.GetComponentInChildren<Text>();

        winOrLoseText.text = "";

        afterActionReport.SetActive(false);
        pauseScreen.SetActive(false);

        healthSlider = GetComponentInChildren<Slider>();
        healthSlider.maxValue = playerHealth.HealthValue;

        gameEventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();

        Time.timeScale = 1;
	}
	
	// Update is called once per frame
	void Update ()
    {
        healthSlider.value = playerHealth.HealthValue;
        if (Input.GetButtonDown(pauseButton))
        {
            Pause();
        }
	}

    private void Pause()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            Time.timeScale = 0;
            pauseScreen.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            pauseScreen.SetActive(false);
        }
    }

    public void UpdateScoreText(int additionalScore)
    {
        score = score + additionalScore;
        scoreText.text = "Score:" + score.ToString();
    }

    public void Win()
    {
        afterActionReport.SetActive(true);
        winOrLoseText.text = "You Win!";
        finalScoreText.text += score.ToString();
        gameEventSystem.SetSelectedGameObject(firstSelectedButton);

        Time.timeScale = 0;
    }

    public void Lose()
    {
        afterActionReport.SetActive(true);
        winOrLoseText.text = "You Lose!";
        finalScoreText.text += score.ToString();
        gameEventSystem.SetSelectedGameObject(firstSelectedButton);

        Time.timeScale = 1;
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
