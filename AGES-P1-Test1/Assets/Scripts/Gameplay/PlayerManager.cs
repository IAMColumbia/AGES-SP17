using System;
using UnityEngine;

[Serializable]
public class PlayerManager
{
    public Color playerColor;
    public Material playerColorMaterial;
    public Transform spawnPoint;
    public int playerNumber;
    public GameObject instance;
    public int wins;

    private PlayerController playerController;
    private GameObject canvasGameObject;

    private Coloring coloring;
    private PlayerHealth health;

    public void Setup()
    {
        playerController = instance.GetComponent<PlayerController>();

        playerController.playerNumber = playerNumber;

        coloring = instance.GetComponentInChildren<Coloring>();

        coloring.playerColor = playerColorMaterial;

        health = instance.GetComponent<PlayerHealth>();
    }


    public void DisableControl()
    {
        playerController.enabled = false;
    }


    public void EnableControl()
    {
        playerController.enabled = true;
        playerController.ResetBoosters();
        playerController.ResetShields();
        health.ResetHealth();
    }


    public void Reset()
    {
        instance.transform.position = spawnPoint.position;
        instance.transform.rotation = spawnPoint.rotation;

        instance.SetActive(false);
        instance.SetActive(true);
    }
}
