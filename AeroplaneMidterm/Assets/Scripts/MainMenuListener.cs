using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuListener : MonoBehaviour {

    public GameObject StartText;
    public GameObject QuitText;

    public GameObject TutorialText;
    public GameObject CheckInText;
    public GameObject CreditsDisplayText;
    public GameObject ReturnText;
    public GameObject CreditsText;
    public GameObject TutorialDisplayText;

    bool displayingCredits = false;
    bool subMenuActive = false;

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
        if (subMenuActive == false && Input.GetKeyDown("joystick 1 button 7"))
        {
            this.gameObject.GetComponent<AudioSource>().Play();
            StartText.SetActive(false);
            QuitText.SetActive(false);
            TutorialText.SetActive(true);
            CheckInText.SetActive(true);
            CreditsDisplayText.SetActive(true);
            CreditsText.SetActive(true);
            ReturnText.SetActive(true);
            subMenuActive = true;
        }

        if (subMenuActive == false && Input.GetKeyDown("joystick 1 button 1"))
        {
            Application.Quit();
        }

        if (subMenuActive == true && Input.GetKeyDown("joystick 1 button 0"))
        {
            SceneManager.LoadScene("CheckIn");
        }

        if (subMenuActive == true && displayingCredits && Input.GetKeyDown("joystick 1 button 2"))
        {
            TutorialText.SetActive(false);
            CreditsText.SetActive(true);
            //TutorialDisplayText.SetActive(true);
        }

        if (subMenuActive == true && Input.GetKeyDown("joystick 1 button 1"))
        {
            StartText.SetActive(true);
            QuitText.SetActive(true);
            TutorialText.SetActive(false);
            CheckInText.SetActive(false);
            CreditsDisplayText.SetActive(false);
            TutorialDisplayText.SetActive(false);
            CreditsText.SetActive(false);
            ReturnText.SetActive(false);
            subMenuActive = false;
        }
    }
}
