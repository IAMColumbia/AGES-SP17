using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenuPlayerAnimation : MonoBehaviour
{
    private enum mainMenuButton { Play, HowToPlay, Credits}

    [SerializeField]
    private Vector3 hoverOverPlay;
    [SerializeField]
    private Vector3 hoverOverHowToPlay;
    [SerializeField]
    private Vector3 hoverOverCredits;

    [SerializeField]
    private Button[] menuButtons;

    private Vector3[] hoverOverTransforms = new Vector3[2];

    mainMenuButton buttonBeingHoveredOver = mainMenuButton.Play;

    BaseEventData eventSystemRef;

    private int currentlySelectedButtonInt;

	// Use this for initialization
	void Start ()
    {
        currentlySelectedButtonInt = 1;
        hoverOverTransforms[0] = hoverOverPlay;
        hoverOverTransforms[1] = hoverOverHowToPlay;
        hoverOverTransforms[2] = hoverOverCredits;
    }
	
	// Update is called once per frame
	void Update ()
    {
        UpdateButtonBeingHoveredOver();
        ChangePositionBasedOnButtonBeingHoveredOver();
	}

    private void UpdateButtonBeingHoveredOver()
    {
        //Debug.Log(Input.GetAxis("Vertical"));
        //if (Input.GetAxis("Vertical") > 0 && currentlySelectedButtonInt <3)
        //{
        //    currentlySelectedButtonInt--;
        //}
        //if (Input.GetAxis("Vertical") < 0 && currentlySelectedButtonInt > 0)
        //{
        //    currentlySelectedButtonInt++;
        //}
        //switch (currentlySelectedButtonInt)
        //{
        //    case 1:
        //        buttonBeingHoveredOver = mainMenuButton.Play;
        //        break;
        //    case 2:
        //        buttonBeingHoveredOver = mainMenuButton.HowToPlay;
        //        break;
        //    case 3:
        //        buttonBeingHoveredOver = mainMenuButton.Credits;
        //        break;
        //    default:
        //        break;
        //}

        for (int i = 0; i < menuButtons.Length; i++)
        {
            if (menuButtons[i])
            {
                gameObject.transform.position = hoverOverTransforms[i];
            }
        }
    }

    private void ChangePositionBasedOnButtonBeingHoveredOver()
    {
        switch (buttonBeingHoveredOver)
        {
            case mainMenuButton.Play:
                currentlySelectedButtonInt = 1;
                gameObject.transform.position = hoverOverPlay;
                break;
            case mainMenuButton.HowToPlay:
                currentlySelectedButtonInt = 2;
                gameObject.transform.position = hoverOverHowToPlay;
                break;
            case mainMenuButton.Credits:
                currentlySelectedButtonInt = 3;
                gameObject.transform.position = hoverOverCredits;
                break;
            default:
                break;
        }
    }
}
