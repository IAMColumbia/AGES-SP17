using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    string sceneToLoad;

    Animator animator;

	// Use this for initialization
	void Start ()
    {
        animator = gameObject.GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	   
	}

    public void StartButtonPressed()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneToLoad); //not sure why but I would continue to get errors unless UnityEngine.Scenemanagement was there, even though I have the using statement.
    }

    public void CreditsButtonPressed()
    {
        animator.SetBool("CreditsButtonPressed", true);
    }

    public void QuitButtonPressed()
    {
        Application.Quit();
    }

    public void ProfileButtonPressed()
    {
        Application.OpenURL("https://www.linkedin.com/in/ryan-carpenter-609261116/");
    }

    public void LevelSpriteButtonPressed()
    {
        Application.OpenURL("https://opengameart.org/content/free-platformer-asset-pack-dungeon");
    }

    public void PlayerSpriteButtonPressed()
    {
        Application.OpenURL("https://opengameart.org/content/2d-game-wizard-character-sprite");
    }

    public void DismissButtonPressed()
    {
        animator.SetBool("CreditsButtonPressed", false);
    }

    public void ParticleButtonPressed()
    {
        Application.OpenURL("https://www.assetstore.unity3d.com/en/#!/content/51503");
    }
}
