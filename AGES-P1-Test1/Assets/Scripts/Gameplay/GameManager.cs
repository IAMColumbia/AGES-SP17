using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour
{
    public int numRoundsToWin = 3;        
    public float startDelay = 3f;         
    public float endDelay = 5f;           
    public CameraController cameraController;   
    public Text messageText;
    public Image backGround;              
    public GameObject playerPrefab;         
    public PlayerManager[] players;           

    [SerializeField]
    private int roundNumber;              
    private WaitForSeconds startWait;     
    private WaitForSeconds endWait;       
    private PlayerManager roundWinner;
    private PlayerManager gameWinner;

    [SerializeField]
    AudioSource audioSource;

    [SerializeField]
    AudioClip[] clips;


    private void Start()
    {
        startWait = new WaitForSeconds(startDelay);
        endWait = new WaitForSeconds(endDelay);

        StartCoroutine(LoadingWait());
    }


    private IEnumerator LoadingWait()
    {
        yield return new WaitForSeconds(2f);
        SpawnAllTanks();
        SetCameraTargets();
        StartCoroutine(GameLoop());
    }


    private void SpawnAllTanks()
    {
        for (int i = 0; i < players.Length; i++)
        {
            players[i].instance =
                Instantiate(playerPrefab, players[i].spawnPoint.position, players[i].spawnPoint.rotation) as GameObject;
            players[i].playerNumber = i + 1;
            players[i].Setup();
        }
    }


    private void SetCameraTargets()
    {
        Transform[] targets = new Transform[players.Length];

        for (int i = 0; i < targets.Length; i++)
        {
            targets[i] = players[i].instance.transform;
        }

        cameraController.targets = targets;
    }


    private IEnumerator GameLoop()
    {
        yield return StartCoroutine(RoundStarting());
        yield return StartCoroutine(RoundPlaying());
        yield return StartCoroutine(RoundEnding());

      if (gameWinner != null)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            StartCoroutine(GameLoop());
        }
    }


    private IEnumerator RoundStarting()
    {
        ResetAllTanks();
        DisableTankControl();
        

        cameraController.SetStartPositionAndSize();

        Color color = backGround.color;
        color.a = 255;
        backGround.color = color;

        roundNumber++;
        messageText.text = "Round " + roundNumber;

        audioSource.clip = clips[3];
        audioSource.Play();

        yield return new WaitForSeconds(audioSource.clip.length);

        ChangeTrack();
    }


    private void ChangeTrack()
    {
        
        if (roundNumber == 1 || roundNumber == 2)
        {
            audioSource.clip = clips[0];
        }

        if (roundNumber == 3 || roundNumber == 4)
        {
            audioSource.clip = clips[1];
        }

        if (roundNumber == 5)
        {
            audioSource.clip = clips[2];
        }

        audioSource.Play();
    }
    

    private IEnumerator RoundPlaying()
    {
        EnableTankControl();

        Color color = backGround.color;
        color.a = 0;
        backGround.color = color;

        messageText.text = string.Empty;

        while (!OneTankLeft())
        {
            yield return null;
        }
    }


    private IEnumerator RoundEnding()
    {
        DisableTankControl();

        roundWinner = null;

        roundWinner = GetRoundWinner();

        if(roundWinner != null)
        {
            roundWinner.wins++;
        }

        Color color = backGround.color;
        color.a = 255;
        backGround.color = color;

        gameWinner = GetGameWinner();

        string message = EndMessage();
        messageText.text = message;

        yield return endWait;
    }


    private bool OneTankLeft()
    {
        int numTanksLeft = 0;

        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].instance.activeSelf)
                numTanksLeft++;
        }

        return numTanksLeft <= 1;
    }


    private PlayerManager GetRoundWinner()
    {
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].instance.activeSelf)
                return players[i];
        }

        return null;
    }


    private PlayerManager GetGameWinner()
    {
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].wins == numRoundsToWin)
                return players[i];
        }

        return null;
    }


    private string EndMessage()
    {
        string message = "DRAW!";

        if (roundWinner != null)
            message = "Player " + roundWinner.playerNumber + " has survived.";

        message += "\n\n\n\n";

        for (int i = 0; i < players.Length; i++)
        {
            message += "P"+ players[i].playerNumber + " : " + players[i].wins + "\n";
        }

        if (gameWinner != null)
            message = "Player " + gameWinner.playerNumber + " is victorious.";

        return message;
    }


    private void ResetAllTanks()
    {
        for (int i = 0; i < players.Length; i++)
        {
            players[i].Reset();
        }
    }


    private void EnableTankControl()
    {
        for (int i = 0; i < players.Length; i++)
        {
            players[i].EnableControl();
        }
    }


    private void DisableTankControl()
    {
        for (int i = 0; i < players.Length; i++)
        {
            players[i].DisableControl();
        }
    }
}