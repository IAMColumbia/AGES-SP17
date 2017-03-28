using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class WinnerUIButtons : MonoBehaviour
{
    [SerializeField]
    private string sceneToLoad;
    [SerializeField]
    private string sceneToReload;
    [SerializeField]
    private string goBackToMainMenu;

    public void NextTrackButtonPressed()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void RetryButtonPressed()
    {
        SceneManager.LoadScene(sceneToReload);
    }

    public void QuitButtonPressed()
    {
        SceneManager.LoadScene(goBackToMainMenu);
    }
}
