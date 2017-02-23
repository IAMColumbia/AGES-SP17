using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    [SerializeField]
    string firstLevelName;

    public void StartGameButtonClicked()
    {
        LoadingScene.loadNewScene(firstLevelName);
    }

}
