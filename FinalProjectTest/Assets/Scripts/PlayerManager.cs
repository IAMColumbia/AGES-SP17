using UnityEngine;
using System;

[Serializable]
public class PlayerManager
{
    public Color playerColor;
    public Transform spawnPoint;
    public int playerNumber;
    public int numberOfWins;
    public GameObject instanceOfPlayer;

    private PlayerMovement playerMovement;
    private PlayerHealth playerHealth;


    public void Setup()
    {
        playerMovement = instanceOfPlayer.GetComponent<PlayerMovement>();

        playerMovement.playerNumber = playerNumber;

        MeshRenderer[] renderers = instanceOfPlayer.GetComponentsInChildren<MeshRenderer>();

        // Go through all the renderers...
        for (int i = 0; i < renderers.Length; i++)
        {
            // ... set their material color to the color specific to this tank.
            renderers[i].material.color = playerColor;
        }
    }

    // Used during the phases of the game where the player shouldn't be able to control their tank.
    public void DisableControl()
    {
        playerMovement.enabled = false;
    }

    // Used during the phases of the game where the player should be able to control their tank.
    public void EnableControl()
    {
        playerMovement.enabled = true;
    }

    public void Reset()
    {
        instanceOfPlayer.transform.position = spawnPoint.position;
        instanceOfPlayer.transform.rotation = spawnPoint.rotation;

        instanceOfPlayer.SetActive(false);
        instanceOfPlayer.SetActive(true);
    }
}
