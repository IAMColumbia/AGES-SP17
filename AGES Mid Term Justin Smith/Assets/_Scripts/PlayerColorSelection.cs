using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerColorSelection : MonoBehaviour
{
    [SerializeField]
    int playerNumber;
    [SerializeField]
    Button[] buttonsThatCanBeAccessed;
    [SerializeField]
    Image playerColorSelectionImage;
    [SerializeField]
    InformationFromMenuToGameManager informationFromMenuToGameManager;

    bool isColorSelected;
    int colorSwitchSelector = 0;
    public int ColorSwitchSelector
    {
        get { return colorSwitchSelector; }
        set { colorSwitchSelector = value; }
    }

    Color[] colorChoices;

	void Start ()
    {
        isColorSelected = false;
        colorChoices = new Color[] { Color.red, Color.green, Color.yellow, Color.cyan, Color.black, Color.magenta, Color.blue };
	}
	
	void Update ()
    {
        //HandleInput();
	}

    void HandleInput()
    {
        if (Input.GetButtonDown("Hortizontal" + playerNumber))
        {

        }
        else if (Input.GetButtonDown("Fire" + playerNumber))
        {

        }
    }
    public void selectColorLeftButton()
    {
        if (colorSwitchSelector > colorChoices.GetLowerBound(0))
        {
            colorSwitchSelector--;
            playerColorSelectionImage.color = colorChoices[colorSwitchSelector];
        }
    }

    public void SelectColorButton()
    {
        foreach (Color colorsSelectedByPlayers in informationFromMenuToGameManager.PlayersColorChoices)
        {
            if (playerColorSelectionImage.color == colorsSelectedByPlayers)
            {
                isColorSelected = true;
                break;
            }
            else
                isColorSelected = false;
        }
        if (!isColorSelected)
            informationFromMenuToGameManager.SetPlayerColor(playerNumber, playerColorSelectionImage.color);
    }

    public void SelectColorRightButton()
    {
        if (colorSwitchSelector < colorChoices.Length)
        {
            colorSwitchSelector++;
            playerColorSelectionImage.color = colorChoices[colorSwitchSelector];
        }
    }
}
