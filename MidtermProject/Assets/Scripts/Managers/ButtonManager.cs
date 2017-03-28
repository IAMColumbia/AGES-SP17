using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    [SerializeField]
    string mainMenuSceneName;
    [SerializeField]
    string creditsSceneName;
    [SerializeField]
    string firstLevelName;

    MenuMusic menuMusic;

    public void StartGameButtonClicked()
    {
        LoadingScene.LoadNewScene(firstLevelName);

        menuMusic = GameObject.FindGameObjectWithTag("MenuMusic").GetComponent<MenuMusic>();
        menuMusic.StopMusic();
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
