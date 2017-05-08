using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StateManager : MonoBehaviour
{
    public enum GameState { MAIN_MENU, IN_GAME, LEVEL_EDITOR, PAUSED }
    static GameState currGameState;

    public enum PlayerState { INACTIVE, WALKING, INTERACTING, DEAD, WON_LEVEL }
    static PlayerState currPlayerState;

    static PlayerState prevPlayerState;
    static GameState prevGameState;

    public static PlayerState previousPlayerState { get { return prevPlayerState; } }
    public static GameState previousGameState { get { return prevGameState; } }

    public static GameState gameState
    {
        get { return currGameState; }

        set
        {
            prevGameState = currGameState;
            currGameState = value;
        }
    }

    public static PlayerState playerState
    {
        get { return currPlayerState; }

        set
        {
            prevPlayerState = currPlayerState;
            currPlayerState = value;
        }
    }

    void Start()
    {
        playerState = PlayerState.INACTIVE;
    }
}
