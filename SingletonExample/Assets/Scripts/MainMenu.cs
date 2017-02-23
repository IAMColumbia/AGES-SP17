using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    [SerializeField]
    private string sceneToLoad;

	public void StartGameButtonClicked()
    {
        LoadingScene.LoadNewScene(sceneToLoad);
    }
}
