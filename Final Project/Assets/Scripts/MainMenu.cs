using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    [SerializeField]
    string firstLevelName;

    [SerializeField]
    string credits;

    public void StartGameButtonClicked()
    {
        SceneManager.LoadScene(firstLevelName);
    }

    public void CreditsButtonClicked()
    {
        SceneManager.LoadScene(credits);
    }

    public void ExitButtonClicked()
    {
        Application.Quit();
    }

}
