using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
    [SerializeField] GameObject InGameCanvas;
    [SerializeField] GameObject LevelEditorCanvas;

    [SerializeField] GameObject PauseMenuPanel;
    [SerializeField] GameObject GameOverPanel;
    [SerializeField] GameObject WinPanel;
    [SerializeField] GameObject LevelSelectPanel;

    #region Save Related Variables
    [SerializeField] GameObject SaveLevelPanel;
    [SerializeField] GameObject MessagePromptPanel;
    [SerializeField] Text MessagePromptPanelText;
    #endregion

    AudioManager audioManager;
    GameObject activePanel;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        if (!audioManager)
        {
            throw new System.Exception("There is no audioManager script in the scene!");
        }

        if (StateManager.gameState == StateManager.GameState.IN_GAME)
        {
            StateManager.gameState = StateManager.GameState.MAIN_MENU;
            TurnOnLevelSelectPanel();
        }
        else if (StateManager.gameState == StateManager.GameState.LEVEL_EDITOR)
        {
            StateManager.gameState = StateManager.GameState.MAIN_MENU;
            TurnOnLevelEditorCanvas();
        }
    }

    #region Loading Canvases
    public void TurnOnInGameCanvas()
    {
        StateManager.gameState = StateManager.GameState.IN_GAME;
        StateManager.playerState = StateManager.PlayerState.WALKING;

        LevelEditorCanvas.SetActive(false);
        InGameCanvas.SetActive(true);

        audioManager.PlayInGameTheme();

        TurnOffActivePanel();
    }

    public void TurnOnLevelEditorCanvas()
    {
        StateManager.gameState = StateManager.GameState.LEVEL_EDITOR;
        StateManager.playerState = StateManager.PlayerState.INACTIVE;

        InGameCanvas.SetActive(false);
        LevelEditorCanvas.SetActive(true);

        audioManager.PlayLevelEditorTheme();

        TurnOffActivePanel();
    }
    #endregion


    #region Saving the Level

    public void TurnOnSaveMenu()
    {
        StateManager.gameState = StateManager.GameState.PAUSED;
        SwapActivePanel(SaveLevelPanel);
    }
    public void ResumeEditing()
    {
        StateManager.gameState = StateManager.GameState.LEVEL_EDITOR;
        TurnOffActivePanel();
    }

    public void AttemptSaveBoard(InputField namingInputField)
    {
        if (namingInputField && namingInputField.text != "")
        {
            Debug.Log("SAVED");
            Board board = FindObjectOfType<Board>();
            board.SaveBoard(namingInputField.text);
            ChangeAndDisplayMessagePromptText("Level Saved!");
            successfullySaved = true;
        }
        else
        {
            Debug.Log("FAILED");
            ChangeAndDisplayMessagePromptText("You must give your level a name!");
        }
    }

    #endregion


    #region In-Game related methods
    public void TurnOnPauseMenu()
    {
        StateManager.gameState = StateManager.GameState.PAUSED;
        SwapActivePanel(PauseMenuPanel);
    }
    public void ResumeGame()
    {
        StateManager.gameState = StateManager.GameState.IN_GAME;
        TurnOffActivePanel();
    }

    public void TurnOnGameOverMenu()
    {
        StateManager.playerState = StateManager.PlayerState.DEAD;
        SwapActivePanel(GameOverPanel);
    }

    public void TurnOnWinMenu()
    {
        StateManager.playerState = StateManager.PlayerState.WON_LEVEL;
        SwapActivePanel(WinPanel);
    }

    public void TurnOnLevelSelectPanel()
    {
        SwapActivePanel(LevelSelectPanel);
        TurnOffInGameAndLevelEditorCanvases();

        LevelSelectPanel.GetComponent<LevelSelectGUI>().Init();
        StateManager.gameState = StateManager.GameState.PAUSED;
    }
    #endregion


    #region Panel Functionality And Message Prompt
    void SwapActivePanel(GameObject panelToActivate)
    {
        if(activePanel)
            activePanel.SetActive(false);

        activePanel = panelToActivate;
        activePanel.SetActive(true);
    }

    public void TurnOffActivePanel()
    {
        if(activePanel)
            activePanel.SetActive(false);
    }



    public void BackButton()
    {
        TurnOffActivePanel();
        TurnOffMessagePrompt();

        switch (StateManager.previousGameState)
        {
            case StateManager.GameState.IN_GAME:
                TurnOnInGameCanvas();
                break;
            case StateManager.GameState.LEVEL_EDITOR:
                TurnOnLevelEditorCanvas();
                break;
            case StateManager.GameState.MAIN_MENU:
                LoadMainScene();
                break;
            case StateManager.GameState.PAUSED:
                TurnOnInGameCanvas();
                break;
            default:
                Debug.Log("Something terrible has happened");
                break;
        }
    }

    bool successfullySaved = false;
    public void TurnOffMessagePrompt()
    {
        if (SaveLevelPanel.activeSelf && successfullySaved)
        {
            successfullySaved = false;
            SaveLevelPanel.SetActive(false);
            StateManager.gameState = StateManager.GameState.LEVEL_EDITOR;
        }

        MessagePromptPanel.SetActive(false);
    }

    public void ChangeAndDisplayMessagePromptText(string desiredText)
    {
        if (MessagePromptPanel && MessagePromptPanelText)
        {
            MessagePromptPanelText.text = desiredText;
            MessagePromptPanel.SetActive(true);
        }
    }

    public void TurnOffInGameAndLevelEditorCanvases()
    {
        InGameCanvas.SetActive(false);
        LevelEditorCanvas.SetActive(false);
    }
    #endregion

    public void LoadMainScene(string mainSceneName = "MainMenu")
    {
        audioManager.PlayMenuTheme();
        SceneManager.LoadScene(mainSceneName);
    }

    public void PlaySFX(AudioClip desiredSFX)
    {
        audioManager.playSFX(desiredSFX);
    }
}
