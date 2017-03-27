using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour {

    public string firstLevelName;

    public void StartGame()
    {
        LoadingScreen.LoadNewScene(firstLevelName);
    }
}
