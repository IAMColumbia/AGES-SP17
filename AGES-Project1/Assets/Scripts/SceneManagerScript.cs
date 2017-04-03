using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class SceneManagerScript : MonoBehaviour {
    [SerializeField]
    AudioSource buttonClicked;


    void Start()
    {
        buttonClicked = GetComponent<AudioSource>();
    }
    public void MainMenuButton()
    {
        buttonClicked.Play();
        SceneManager.LoadScene("Main Menu");
    }

    public void ExitGameButton()
    {
        buttonClicked.Play();
        Application.Quit();
    }
    public void CreditsButton()
    {
        buttonClicked.Play();
        SceneManager.LoadScene("Credits");
    }
    public void EnterGame()
    {
        buttonClicked.Play();
        SceneManager.LoadScene("Level");
    }
}
