using UnityEngine;
using System.Collections;

public class ButtonManager : MonoBehaviour
{
    void PlayGame()
    {
        //change to game scene

        Debug.Log("Changed to game scene.");
    }

    void ViewCredits()
    {
        //change to credits scene

        Debug.Log("Changed to credits scene.");
    }

    void ReturnToMainMenu()
    {
        //change to menu scene

        Debug.Log("Changed to menu scene.");
    }

    void ExitGame()
    {
        Application.Quit();
    }
}
