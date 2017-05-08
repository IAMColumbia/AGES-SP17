using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIManager : MonoBehaviour
{

    [SerializeField] GameObject instructionsPanel;
    [SerializeField] GameObject creditsPanel;

    private void Start()
    {
        instructionsPanel.SetActive(false);
        creditsPanel.SetActive(false);
    }

    public void OnPlayClick()
    {
        SceneManager.LoadScene("Level1");
    }

    public void OnInstructionsClick()
    {
        instructionsPanel.SetActive(true);
    }

    public void OnCreditsClick()
    {
        creditsPanel.SetActive(true);
    }

    public void OnQuitClick()
    {
        Application.Quit();
    }

    public void OnBackClick()
    {
        instructionsPanel.SetActive(false);
        creditsPanel.SetActive(false);
    }

    public void OnReplayClick()
    {
        GameManager.Instance.ReloadLevel();
    }

    public void OnBackToMenuClick()
    {
        GameManager.Instance.curLevel = 0;
        SceneManager.LoadScene("Main Menu");
    }

    public void OnURLClick()
    {
        Application.OpenURL("http://www.bensound.com/");
    }

}
