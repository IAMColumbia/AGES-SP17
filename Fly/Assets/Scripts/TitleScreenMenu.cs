using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class TitleScreenMenu : MonoBehaviour {

    // Use this for initialization
    [SerializeField]
    GameObject creditScreen;

    [SerializeField]
    GameObject mainMenuScreen;

    [SerializeField]
    GameObject instructionsScreen;

    public AudioSource audioSource;

	void Start () {

        creditScreen.SetActive(false);
        instructionsScreen.SetActive(false);

    }
	
	// Update is called once per frame
	public void ShowCreditScreen()
    {
        mainMenuScreen.SetActive(false);
        creditScreen.SetActive(true);
    }
    public void HideCreditScreen()
    {
        creditScreen.SetActive(false);
        mainMenuScreen.SetActive(true);
    }

    public void ShowInstructionsScreen()
    {
        mainMenuScreen.SetActive(false);
        instructionsScreen.SetActive(true);
    }
    public void HideInstructionsScreen()
    {
        mainMenuScreen.SetActive(true);
        instructionsScreen.SetActive(false);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void clickSound()
    {
        audioSource.Play();
    }
}
