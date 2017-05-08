using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    string firstLevelName;

    [SerializeField]
    string credits;

    [SerializeField]
    string instruction;

    public void StartGameButtonClicked()
    {
        LoadingScene.LoadNewScene(firstLevelName);
    }

    public void CreditsButtonClicked()
    {
        LoadingScene.LoadNewScene(credits);
    }

    public void InstructionButtonClicked()
    {
        LoadingScene.LoadNewScene(instruction);
    }

    public void ExitButtonClicked()
    {
        Application.Quit();
    }
}
