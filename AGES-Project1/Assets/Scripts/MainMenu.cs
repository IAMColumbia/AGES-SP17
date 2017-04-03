using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    AudioSource buttonClicked;
    
    void Start()
    {
        buttonClicked = GetComponent<AudioSource>();
    }
    public void StartGameButton()
    {
        buttonClicked.Play();
        SceneManager.LoadScene("How To Play");
    }
	
    
}
