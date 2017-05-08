using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BasicCharacter
{
    GUIManager guiManager;
    [SerializeField] ParticleSystem winParticleSystem;

    private void Awake()
    {
        guiManager = GameObject.FindObjectOfType<GUIManager>();
    }

    public override void Kill()
    {
        if(canDie)
        {
            //Kill the player
            guiManager.TurnOnGameOverMenu();
        }
    }


    public void WinLevel()
    {
        guiManager.TurnOnWinMenu();
        Camera.main.gameObject.GetComponent<WinParticleEffects>().ActivateWinEffect();
    }
}
