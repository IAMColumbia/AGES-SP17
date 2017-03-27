using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    #region SerializedFields
    [SerializeField]
    private GameObject mainMenuPanel;
    [SerializeField]
    private GameObject instructionsPanel;
    [SerializeField]
    private GameObject creditsPanel;
    #endregion

    public void OpenInstructions()
    {
        instructionsPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
        creditsPanel.SetActive(false);
    }

    public void OpenMainMenu()
    {
        mainMenuPanel.SetActive(true);
        instructionsPanel.SetActive(false);
        creditsPanel.SetActive(false);
    }

    public void OpenCredits()
    {
        mainMenuPanel.SetActive(false);
        instructionsPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }
}
