using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoaderListener : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        InputListener();	
	}

    void InputListener()
    {
        if (Input.GetKeyDown("joystick 1 button 0"))
        {
            Debug.Log("Button pressed");
            SceneManager.LoadScene("PlaneScene");
        }
    }
}
