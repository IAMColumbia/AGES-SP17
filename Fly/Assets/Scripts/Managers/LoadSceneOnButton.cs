using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadSceneOnButton : MonoBehaviour {

	// Use this for initialization
	public void LoadByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
