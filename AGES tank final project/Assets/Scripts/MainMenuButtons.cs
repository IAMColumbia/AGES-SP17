using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class MainMenuButtons : MonoBehaviour
{
    [SerializeField]
    private string ScenetoLoad;

    public void LoadScene()
    {
        SceneManager.LoadScene(ScenetoLoad);
    }
    
    public void QuitButtonIsPressed()
    {
        Application.Quit();
    }
}
