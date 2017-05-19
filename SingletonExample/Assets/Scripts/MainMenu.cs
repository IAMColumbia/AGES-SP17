using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	// Use this for initialization
    [SerializeField]
    string firstLevelName;
	public void StartGameButtonClicked()
    {
        SceneManager.LoadScene("loadingScene"); 
    }

}
