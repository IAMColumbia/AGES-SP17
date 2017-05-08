using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuButtonManager : MonoBehaviour
{
    [SerializeField]
    string mainMenuSceneName;
    [SerializeField]
    string creditsSceneName;
    [SerializeField]
    string firstLevelSceneName;

    public void StartGameButtonClicked()
    {
        SceneManager.LoadScene(firstLevelSceneName);
    }

    public void CreditsButtonClick()
    {
        SceneManager.LoadScene(creditsSceneName);
    }

    public void MainMenuButtonClicked()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }

    public void ExitGameButtonClicked()
    {
        Application.Quit();
    }
}
