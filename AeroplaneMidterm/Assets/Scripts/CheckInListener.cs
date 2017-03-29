using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckInListener : MonoBehaviour {

    private bool player1Check = false;
    private bool player2Check = false;

    public GameObject Player1Text;
    public GameObject Player2Text;
    public GameObject StartText;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        SeatCheck();
	}

    void SeatCheck()
    {
        if (Input.GetKeyDown("joystick 1 button 0"))
        {
            this.gameObject.GetComponent<AudioSource>().Play();
            Debug.Log("Player 1 checked in.");
            Player1Text.SetActive(true);
            player1Check = true;
        }
        if (Input.GetKeyDown("joystick 2 button 0"))
        {
            this.gameObject.GetComponent<AudioSource>().Play();
            Debug.Log("Player 2 checked in.");
            Player2Text.SetActive(true);
            player2Check = true;
        }

        if (player1Check && player2Check)
        {
            Debug.Log("Players checked in.");
            StartText.SetActive(true);
        }

        if (Input.GetKeyDown("joystick 1 button 7") && player1Check && player2Check)
        {
            SceneManager.LoadScene("Level Select");
        }
    }
}
