using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ButtonScitps : MonoBehaviour {

	public void OnPlayClick ()
    {

        SceneManager.LoadScene("Loading");
    }

    public void MainMenuClick ()
    {

        SceneManager.LoadScene(0);
    }

    public void CreditsCLick ()
    {

        SceneManager.LoadScene("Credits");
    }

    public void Exit ()
    {
        Application.Quit();
    }

    public void GoToMainmenu ()
    {

        SceneManager.LoadScene("MainMenu");
    }
}
