using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    private string cancelButton;

    private GameObject TitlePanel;
    private GameObject CreditsPanel;
    private GameObject HowToPlayPanel;

	// Use this for initialization
	void Start ()
    {
        TitlePanel = GameObject.Find("TitlePanel");
        CreditsPanel = GameObject.Find("CreditsPanel");
        HowToPlayPanel = GameObject.Find("HowToPlayPanel");

        CloseInstuctionsAndCredits();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButtonDown(cancelButton))
        {
            CloseInstuctionsAndCredits();
        }
	}

    public void LoadScene(string sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void OpenCreditsPanel()
    {
        TitlePanel.SetActive(false);
        CreditsPanel.SetActive(true);
        HowToPlayPanel.SetActive(false);
    }

    public void OpenHowToPlayPanel()
    {
        TitlePanel.SetActive(false);
        CreditsPanel.SetActive(false);
        HowToPlayPanel.SetActive(true);
    }

    public void CloseInstuctionsAndCredits()
    {
        TitlePanel.SetActive(true);
        CreditsPanel.SetActive(false);
        HowToPlayPanel.SetActive(false);
    }
}
