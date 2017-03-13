using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class MenuButtons : MonoBehaviour
{
    [SerializeField]
    GameObject startPanel;
    [SerializeField]
    GameObject playerSelectPanel;
    [SerializeField]
    GameObject creditsPanel;
    [SerializeField]
    GameObject weaponSelectPanel;
    [SerializeField]
    Toggle readyToggle1;
    [SerializeField]
    Toggle readyToggle2;
    [SerializeField]
    Toggle readyToggle3;
    [SerializeField]
    Toggle readyToggle4;

    List<Toggle> readyToggles = new List<Toggle>();

    private void Start()
    {

    }

    public void StartButtonPressed()
    {
        playerSelectPanel.SetActive(true);
        startPanel.SetActive(false);
    }

    public void CreditsButtonPressed()
    {
        creditsPanel.SetActive(true);
    }

    public void BackCreditsButtonPressed()
    {
        creditsPanel.SetActive(false);
    }

    public void QuitButtonPressed()
    {
        Application.Quit();
    }

    public void TwoPlayersButtonPressed()
    {
        //number of players = 2
        readyToggles.Add(readyToggle1);
        readyToggles.Add(readyToggle2);

        foreach (Toggle readyToggle in readyToggles)
        {
            readyToggle.gameObject.SetActive(true);
        }

        weaponSelectPanel.SetActive(true);
        playerSelectPanel.SetActive(false);
    }

    public void ThreePlayersButtonPressed()
    {
        //number of players = 3
        readyToggles.Add(readyToggle1);
        readyToggles.Add(readyToggle2);
        readyToggles.Add(readyToggle3);

        foreach (Toggle readyToggle in readyToggles)
        {
            readyToggle.gameObject.SetActive(true);
        }

        weaponSelectPanel.SetActive(true);
        playerSelectPanel.SetActive(false);
    }

    public void FourPlayersButtonPressed()
    {
        //number of players = 4
        readyToggles.Add(readyToggle1);
        readyToggles.Add(readyToggle2);
        readyToggles.Add(readyToggle3);
        readyToggles.Add(readyToggle4);

        foreach (Toggle readyToggle in readyToggles)
        {
            readyToggle.gameObject.SetActive(true);
        }

        weaponSelectPanel.SetActive(true);
        playerSelectPanel.SetActive(false);
    }

    public void StartGame()
    {
        int numberOfTogglesOn = 0;

        foreach (Toggle readyToggle in readyToggles)
        {
            if (readyToggle.isOn)
            {
                numberOfTogglesOn++;
            }
        }

        if (numberOfTogglesOn == readyToggles.Count)
        {
            Debug.Log("start game");
        }
    }
}
