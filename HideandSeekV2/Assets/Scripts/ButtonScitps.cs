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


    public void ONCOREYCLICK ()
    {

        Application.OpenURL("https://www.linkedin.com/in/corey-king2018");
    }

    public void OnMusicClick ()
    {

        Application.OpenURL("https://www.soundcloud.com/no-audible-dialogue");
    }

    public void SoundEffects ()
    {
        Application.OpenURL("https://www.assetstore.unity3d.com/en/#!/content/53575");

    }
}
