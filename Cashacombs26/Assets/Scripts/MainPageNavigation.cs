using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainPageNavigation : MonoBehaviour
{
    AudioManager audioManager;
    GameObject activePanel;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        if (!audioManager)
        {
            throw new System.Exception("There is no audioManager script in the scene!");
        }
    }

    public void LoadSceneInGame(string sceneToLoad = "GameScene")
    {
        StateManager.gameState = StateManager.GameState.IN_GAME;
        SceneManager.LoadScene(sceneToLoad);
    }

    public void LoadSceneLevelEditor(string sceneToLoad = "GameScene")
    {
        StateManager.gameState = StateManager.GameState.LEVEL_EDITOR;
        SceneManager.LoadScene(sceneToLoad);
    }

    public void ShowPane(RectTransform targetPanel)
    {
        activePanel = targetPanel.gameObject;
        activePanel.SetActive(true);
    }

    public void HideActivePanel()
    {
        if (activePanel)
        {
            activePanel.SetActive(false);
            activePanel = null;
        }
    }

    public void PlayButtonSFX(AudioClip desiredSFX)
    {
        audioManager.playSFX(desiredSFX);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ClickableLink(string URL)
    {
        Application.OpenURL(URL);
    }
}
