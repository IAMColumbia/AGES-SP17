using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField]
    string nextScene;
    [SerializeField]
    GameObject creditsImage;
    [SerializeField]
    GameObject quitDialogueImage;
    [SerializeField]
    Image fadeOutImage;
    [SerializeField]
    AudioSource musicLoop;

    public void StartButtonPressed()
    {
        StartCoroutine(FadeOutToNextScene());
    }

    IEnumerator FadeOutToNextScene()
    {
        fadeOutImage.gameObject.SetActive(true);

        for (int i = 0; i < 100; i++)
        {
            fadeOutImage.color = new Color(fadeOutImage.color.r, fadeOutImage.color.g, fadeOutImage.color.b, fadeOutImage.color.a + 0.01f);
            musicLoop.volume -= 0.01f;
            yield return new WaitForSeconds(Time.deltaTime);
        }

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
