using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour 
{
	[SerializeField] string sceneToLoad;

	public void LoadAScene()
	{
		SceneManager.LoadScene (sceneToLoad);
	}

	public void QuitTheGame()
	{
		Application.Quit ();
	}
}
