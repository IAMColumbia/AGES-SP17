using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ReplayOrQuit : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetButtonDown ("Submit")) 
		{
			SceneManager.LoadScene (1);
		}

		if (Input.GetButtonDown ("Cancel")) 
		{
			SceneManager.LoadScene (0);
		}
	
	}
}
