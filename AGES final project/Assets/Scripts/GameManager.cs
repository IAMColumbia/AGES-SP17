using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    Image gameEndPanel;
    [SerializeField]
    GameObject youWinText;
    [SerializeField]
    GameObject pausePanel;
    [SerializeField]
    string sceneToLoad;
    [SerializeField]
    string restartSceneToLoad;
    [SerializeField]
    SwitchWorlds switchWorlds;

    bool isPaused = false;

    public Transform currentCheckpoint;
    public bool ReachedGoal = false;

	// Use this for initialization
	void Start ()
    {
        gameEndPanel.canvasRenderer.SetAlpha(0);
        pausePanel.SetActive(false);
        HandlePause();
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(ReachedGoal == true)
        {
            HandleWinning();
        }

        HandlePause();
	
	}

    private void HandleWinning()
    {
        gameEndPanel.CrossFadeAlpha(1, 1, false);
        youWinText.SetActive(true);
    }

    private void HandlePause()
    {
        if(Input.GetButtonDown("Cancel") && isPaused == false)
        {
            isPaused = true;
            pausePanel.SetActive(true);
            Time.timeScale = 0;
            switchWorlds.enabled = false;
        }
        else if(Input.GetButtonDown("Cancel") && isPaused)
        {
            isPaused = false;
            pausePanel.SetActive(false);
            Time.timeScale = 1;
            switchWorlds.enabled = true;
        }
    }

    public void ContinueButtonPressed()
    {
        isPaused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void RestartButtonPressed()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(restartSceneToLoad);
    }

    public void MainMenuButtonPressed()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneToLoad);
    }
}
