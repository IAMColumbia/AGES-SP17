using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    string firstLevelName;

    public void StartGameButton()
    {

        LoadingScreen.LoadNewScene(firstLevelName);
    }
	
}
