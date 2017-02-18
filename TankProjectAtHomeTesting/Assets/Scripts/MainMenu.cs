using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Tooltip("Make sure level to load is added in build settings!")]
    [SerializeField]
    string levelToLoad;

    public void StartButtonClicked()
    {
        LoadingScreen.Instance.LoadScene(levelToLoad);
    }

    public void AltStartButtonClicked()
    {
        LoadingSceneManager.LoadNewScene("test with terrain");
    }
}
