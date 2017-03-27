using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIManager : MonoBehaviour
{

    [SerializeField] GameObject creditsPanel;

    public void OnPlayClick()
    {
        SceneManager.LoadScene("Player Select");
    }

    public void OnPlayTwoClick()
    {
        SceneManager.LoadScene("Main");
    }

    public void OnCreditsClick()
    {
        SceneManager.LoadScene("Credits");
    }

    public void OnBackToMenuClick()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void OnQuitClick()
    {
        Application.Quit();
    }

}
