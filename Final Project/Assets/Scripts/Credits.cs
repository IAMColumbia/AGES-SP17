using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("space"))
        {
            SceneManager.LoadScene("Title");
        }
        if (Input.GetButtonDown("Fire1"))
        {
            SceneManager.LoadScene("Title");
        }
	}
}
