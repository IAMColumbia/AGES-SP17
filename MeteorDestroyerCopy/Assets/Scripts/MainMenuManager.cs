using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    private string cancelButton;
    [SerializeField]
    private GameObject TitlePanelFirstSelected;
    [SerializeField]
    private GameObject CreditsPanelFirstSelected;
    [SerializeField]
    private GameObject HowToPlayPanelFirstSelected;

    private GameObject TitlePanel;
    private GameObject CreditsPanel;
    private GameObject HowToPlayPanel;

    private EventSystem mainEventSystem;

	// Use this for initialization
	void Start ()
    {
        TitlePanel = GameObject.Find("TitlePanel");
        CreditsPanel = GameObject.Find("CreditsPanel");
        HowToPlayPanel = GameObject.Find("HowToPlayPanel");
        mainEventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();

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

        mainEventSystem.SetSelectedGameObject(CreditsPanelFirstSelected);
    }

    public void OpenHowToPlayPanel()
    {
        TitlePanel.SetActive(false);
        CreditsPanel.SetActive(false);
        HowToPlayPanel.SetActive(true);

        mainEventSystem.SetSelectedGameObject(HowToPlayPanelFirstSelected);
    }

    public void CloseInstuctionsAndCredits()
    {
        TitlePanel.SetActive(true);
        CreditsPanel.SetActive(false);
        HowToPlayPanel.SetActive(false);

        mainEventSystem.SetSelectedGameObject(TitlePanelFirstSelected);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void OpenAssetSourceLink(string sourceLinkURL)
    {
        Application.OpenURL(sourceLinkURL);
    }
}
