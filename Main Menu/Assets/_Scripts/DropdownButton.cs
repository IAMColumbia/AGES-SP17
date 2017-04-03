using UnityEngine;
using System;
using UnityEngine.UI;

public class DropdownButton : MonoBehaviour
{
    [SerializeField]
    GameObject helpButton;
    [SerializeField]
    GameObject audioButton;
    [SerializeField]
    GameObject quitButton;
    [SerializeField]
    GameObject players2Button;
    [SerializeField]
    GameObject players3Button;
    [SerializeField]
    GameObject players4Button;
    [SerializeField]
    Text playerSelectText;

    bool isOpen;

    void Start()
    {
        isOpen = false;
    }
    
    public void OnPlayerSelectButtonClick()
    {
        if (isOpen)
        {
            players2Button.SetActive(false);
            players3Button.SetActive(false);
            players4Button.SetActive(false);

            helpButton.SetActive(true);
            audioButton.SetActive(true);
            quitButton.SetActive(true);
            playerSelectText.text = "Player Select";
        }
        else
        {
            helpButton.SetActive(false);
            audioButton.SetActive(false);
            quitButton.SetActive(false);

            players2Button.SetActive(true);
            players3Button.SetActive(true);
            players4Button.SetActive(true);
            playerSelectText.text = "Back";
        }
        isOpen = !isOpen;
    }
}
