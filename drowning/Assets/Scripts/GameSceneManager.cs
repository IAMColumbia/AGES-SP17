using UnityEngine;
using System.Collections;

public class GameSceneManager : MonoBehaviour {

    public static GameSceneManager instance = null;

    // Use this for initialization
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void GoToMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void GoToGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
