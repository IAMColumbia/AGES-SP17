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
        BackgroundMusic music = GameObject.Find("BackgroundMusic").GetComponent<BackgroundMusic>();
        music.StopMusic();

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

    public void MusicButtonClicked()
    {
        Application.OpenURL("http://incompetech.com/music/royalty-free/music.html");
    }

    public void SFXButtonClicked()
    {
        Application.OpenURL("https://stuckeast.itch.io/");
    }

    public void CharacterButtonClicked()
    {
        Application.OpenURL("https://lionheart963.itch.io/");
    }

    public void BackgroundButtonClicked()
    {
        Application.OpenURL("https://edermunizz.itch.io/");
    }

    public void ExitGameButtonClicked()
    {
        Application.Quit();
    }
}
