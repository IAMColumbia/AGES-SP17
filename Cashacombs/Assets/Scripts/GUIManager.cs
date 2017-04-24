using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIManager : MonoBehaviour
{
    [SerializeField] GameObject InGameCanvas;
    [SerializeField] GameObject LevelEditorCanvas;

    [SerializeField] GameObject PauseMenuPanel;
    [SerializeField] GameObject GameOverMenu;

    GameObject activePanel;

    public void TurnOnInGameCanvas()
    {
        StateManager.gameState = StateManager.GameState.IN_GAME;
        StateManager.playerState = StateManager.PlayerState.WALKING;

        LevelEditorCanvas.SetActive(false);
        InGameCanvas.SetActive(true);

        TurnOffActivePanel();
    }

    public void TurnOnLevelEditorCanvas()
    {
        StateManager.gameState = StateManager.GameState.LEVEL_EDITOR;
        StateManager.playerState = StateManager.PlayerState.INACTIVE;

        InGameCanvas.SetActive(false);
        LevelEditorCanvas.SetActive(true);

        TurnOffActivePanel();
    }

    public void TurnOnPauseMenu()
    {
        StateManager.gameState = StateManager.GameState.PAUSED;
        PauseMenuPanel.SetActive(true);

        SwapActivePanel(PauseMenuPanel);
    }

    public void TurnOnGameOverMenu()
    {
        SwapActivePanel(GameOverMenu);
    }



    void SwapActivePanel(GameObject panelToActivate)
    {
        if(activePanel)
            activePanel.SetActive(false);

        activePanel = panelToActivate;
        activePanel.SetActive(true);
    }

    void TurnOffActivePanel()
    {
        if(activePanel)
            activePanel.SetActive(false);
    }
}
