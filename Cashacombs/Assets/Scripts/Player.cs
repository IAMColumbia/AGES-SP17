using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BasicCharacter
{
    GUIManager guiManager;  //TODO: This should probably be in controller or something...

    private void Awake()
    {
        guiManager = GameObject.FindObjectOfType<GUIManager>();
    }

    public override void Kill()
    {
        if(canDie)
        {
            //Kill the player
            StateManager.playerState = StateManager.PlayerState.DEAD;
            guiManager.TurnOnGameOverMenu();

            Debug.Log(this.gameObject + " has died");
        }

    }
}
