using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour {


    [SerializeField]
    private Text messageText;

    [SerializeField]
    private GameObject[] PlayerPrefabs;

    public PlayerManager[] Players;

    [SerializeField]
    private int buttsToBlast = 15;

    [SerializeField]
    private float startDelay = 5f;

    [SerializeField]
    private float endDelay = 5f;

    [SerializeField]
    private float respawnDelay = 3f;

    [SerializeField]
    private Sprite[] normalSprites;

    [SerializeField]
    private Sprite[] deathSprites;

    [SerializeField]
    private Image[] PlayerSpriteImage;
    
    private CameraControl CameraControl;
    private WaitForSeconds startWait;
    private WaitForSeconds endWait;
    private WaitForSeconds respawnWait;
    private PlayerManager gameWinner;

    void Start() {
        startWait = new WaitForSeconds(startDelay);
        endWait = new WaitForSeconds(endDelay);
        respawnWait = new WaitForSeconds(respawnDelay);
        
        CameraControl = GameObject.FindObjectOfType<CameraControl>();

        SpawnAllPlayers();
        SetCameraTargets();
        StartCoroutine(GameLoop());
        
    }

    private void SpawnAllPlayers()
    {
        for (int i = 0; i < Players.Length; i++)
        {
            Players[i].Instance = Instantiate(PlayerPrefabs[i], Players[i].SpawnPoint.position, Players[i].SpawnPoint.rotation) as GameObject;
            Players[i].PlayerNumber = i + 1;
            Players[i].Setup();

        }
    }

    public IEnumerator RespawnPlayers()
    {
        UpdateUISpritesToDeathSprites();

        yield return respawnWait;

        for (int i = 0; i < Players.Length; i++)
        {
            if (!Players[i].Instance.activeSelf)
            {
                Players[i].Instance = Instantiate(PlayerPrefabs[i], Players[i].SpawnPoint.position, Players[i].SpawnPoint.rotation) as GameObject;
                Players[i].PlayerNumber = i + 1;
                Players[i].Reset();
                SetCameraTargets();
                UpdateUISpritesToNormalSprites();
                Players[i].Instance.GetComponentInChildren<PlayerHealth>().StartCoroutine("playerFlashAndDisableShooting");
            }
        }
        yield return null;
    }

    private IEnumerator GameLoop()
    {
        yield return StartCoroutine(GameStarting());
        yield return StartCoroutine(GamePlaying());
        yield return StartCoroutine(GameEnding());
    }

    private IEnumerator GameEnding()
    {
        DisablePlayerControl();
        
        gameWinner = GetGameWinner();
        
        string message = EndMessage();
        messageText.text = message;

        yield return endWait;
    }

    private IEnumerator GamePlaying()
    {
        EnablePlayerControl();

        messageText.text = string.Empty;
        
        while(GetGameWinner() == null)
            yield return null;
        
    }

    private IEnumerator GameStarting()
    {
        DisablePlayerControl();
        CameraControl.SetStartPositionAndSize();
        
        messageText.text = "BLAST BUTTS";

        yield return startWait;
    }

    private PlayerManager GetGameWinner()
    {
        for (int i = 0; i < Players.Length; i++)
        {
            if (Players[i].ButtsBlasted == buttsToBlast)
                return Players[i];
        }

        return null;
    }
    
    private string EndMessage()
    {
        string message = "DRAW!";

        message += "\n\n\n\n";

        for (int i = 0; i < Players.Length; i++)
        {
            message += Players[i].PlayerColorText + ": " + Players[i].ButtsBlasted + " WINS\n";
        }

        if (gameWinner != null)
            message = gameWinner.PlayerColorText + " WINS THE GAME!";

        return message;
    }

    private void SetCameraTargets()
    {
        Transform[] players = new Transform[Players.Length];

        for (int i = 0; i < players.Length; i++)
        {
            players[i] = Players[i].Instance.transform;
        }

        CameraControl.Targets = players;
    }

    public void UpdateUISpritesToDeathSprites()
    {
        for (int i = 0; i < Players.Length; i++)
        {
            if(!Players[i].Instance.activeSelf)
                PlayerSpriteImage[i].sprite = deathSprites[i];
        }
    }
    public void UpdateUISpritesToNormalSprites()
    {
        for (int i = 0; i < Players.Length; i++)
        {
            PlayerSpriteImage[i].sprite = normalSprites[i];
        }
    }

    public void UpdateUIButtsBlasted()
    {
        for (int i = 0; i < Players.Length; i++)
        {
            Players[i].scoreText.text = "X " + Players[i].ButtsBlasted;
        }
    }

    private void EnablePlayerControl()
    {
        for (int i = 0; i < Players.Length; i++)
        {
            Players[i].EnableControl();
        }
    }


    private void DisablePlayerControl()
    {
        for (int i = 0; i < Players.Length; i++)
        {
            Players[i].DisableControl();
        }
    }

}
