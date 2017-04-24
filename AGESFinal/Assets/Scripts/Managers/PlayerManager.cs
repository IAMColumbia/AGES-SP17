using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
[Serializable]
public class PlayerManager {

    public Transform SpawnPoint;
    public Color PlayerColor;
    //[HideInInspector]
    public float PlayerNumber;
    //[HideInInspector]
    public GameObject Instance;
    //[HideInInspector]
    public int ButtsBlasted;
    //[HideInInspector]
    public string PlayerColorText;
    //[HideInInspector]
    public Text scoreText;

    private PlayerController movement;
    private PlayerShooting shooting;
    private PlayerHealth playerHealth;

    public void Setup()
    {
        movement = Instance.GetComponent<PlayerController>();
        shooting = Instance.GetComponent<PlayerShooting>();

        movement.playerNumber = PlayerNumber;

        PlayerColorText = "<color=#" + ColorUtility.ToHtmlStringRGB(PlayerColor) + ">PLAYER " + PlayerNumber + "</color>";

        scoreText = GameObject.Find("P" + PlayerNumber + "ScoreText").GetComponent<Text>();

    }

    public void DisableControl()
    {
        movement.enabled = false;
        shooting.enabled = false;
    }

    public void EnableControl()
    {
        movement.enabled = true;
        shooting.enabled = true;
    }

    public void Reset()
    {
        if(Instance != null)
        {
            Instance.transform.position = SpawnPoint.position;
            Instance.transform.rotation = SpawnPoint.rotation;

            Instance.SetActive(false);
            Instance.SetActive(true);
        }
    }
}
