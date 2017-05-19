using UnityEngine;
using System.Collections;
using System;

public class EndGameCamera : MonoBehaviour {

    // Use this for initialization

    [SerializeField]
    GameObject mainCamera;
    [SerializeField]
    GameObject endGameCamera;

    [SerializeField]
    GameObject gameWinner;

    bool GameWinner
    {
        get
        {
            return gameWinner.activeSelf;
        }
    }
    void Start () {
        mainCamera.SetActive(true);
        endGameCamera.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        checkGameState();
	
	}
    private void checkGameState()
    {
       if(GameWinner == true)
        {
            endGameCamera.SetActive(true);
            mainCamera.SetActive(false);
        }
    }
}
