using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField]
    string nextScene;
    [SerializeField]
    GameObject creditsImage;
    [SerializeField]
    GameObject quitDialogueImage;

    public void StartButtonPressed()
    {
        SceneManager.LoadScene(nextScene);
    }

    public void CreditsButtonPressed()
    {
        creditsImage.SetActive(true);
    }

    public void QuitButtonPressed()
    {
        quitDialogueImage.SetActive(true);
    }

    public void CreditsBackButtonPressed()
    {
        creditsImage.SetActive(false);
    }

    public void QuitCancelButtonPressed()
    {
        quitDialogueImage.SetActive(false);
    }

    public void QuitQuitButtonPressed()
    {
        Application.Quit();
    }
}
