using System;
using UnityEngine;

[Serializable]
public class PlayerManager
{
    public Color playerColor;
    public Transform spawnPoint;
    [HideInInspector]
    public int playerNumber;
    [HideInInspector]
    public string coloredPlayerText;
    [HideInInspector]
    public GameObject instance;
    [HideInInspector]
    public int wins;


    private PlayerMovement movement;
    //private GameObject canvasGameObject;


    public void Setup()
    {
        movement = instance.GetComponent<PlayerMovement>();
        //canvasGameObject = instance.GetComponentInChildren<Canvas>().gameObject;

        movement.playerNumber = playerNumber;

        coloredPlayerText = "<color=#" + ColorUtility.ToHtmlStringRGB(playerColor) + ">PLAYER " + playerNumber + "</color>";

        MeshRenderer[] renderers = instance.GetComponentsInChildren<MeshRenderer>();

        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material.color = playerColor;
        }
    }


    public void DisableControl()
    {
        movement.enabled = false;

        //canvasGameObject.SetActive(false);
    }


    public void EnableControl()
    {
        movement.enabled = true;

        //canvasGameObject.SetActive(true);
    }


    public void Reset()
    {
        instance.transform.position = spawnPoint.position;
        instance.transform.rotation = spawnPoint.rotation;

        instance.SetActive(false);
        instance.SetActive(true);
    }
}