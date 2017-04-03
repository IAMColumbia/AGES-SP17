using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadGameStart : MonoBehaviour {

	public void LoadGameScreen(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
