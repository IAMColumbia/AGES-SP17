using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//TODO: USE SINGLETON (although I should probably find a way to avoid this)
public class StateManager : MonoBehaviour
{
    public enum GameState { IN_GAME, LEVEL_EDITOR, PAUSED }
    static GameState currGameState;

    public enum PlayerState { WALKING, INTERACTING, DEAD, WON_LEVEL, INACTIVE }
    static PlayerState currPlayerState;

    public static GameState gameState
    {
        get { return currGameState; }

        set { currGameState = value; }
    }

    public static PlayerState playerState
    {
        get { return currPlayerState; }

        set { currPlayerState = value; }
    }

    void Start()
    {
        gameState = GameState.LEVEL_EDITOR;
        playerState = PlayerState.INACTIVE;
    }
}
