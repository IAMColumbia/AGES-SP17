using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public int RoundsToWin = 3;
    public float StartDelay = 3f;
    public float EndDelay = 3f;
    public GameObject PlayerPrefab;
    public Text MessageText;
    public AudioSource Audio;
    public AudioClip[] RoundSounds;
    public List<PlayerManager> Players;
    public GameObject[] WallSections;

    private enum WallSectionStates { FourPlayer, ThreePlayer, TwoPlayer };
    private WallSectionStates WallSectionCurrentState;
    private int roundNumber;
    private WaitForSeconds startWait;
    private WaitForSeconds endWait;
    private PlayerManager roundWinner;
    private PlayerManager gameWinner;

	void Start () {
        startWait = new WaitForSeconds(StartDelay);
        endWait = new WaitForSeconds(EndDelay);

        SpawnAllPlayers();
        WallSectionCurrentState = WallSectionStates.FourPlayer;
        StartCoroutine(GameLoop());
	}
    private void UpdateWallSectionState()
    {
        switch (WallSectionCurrentState)
        {
            case WallSectionStates.FourPlayer:
                WallSections[0].SetActive(true);
                WallSections[1].SetActive(true);
                
                break;
            case WallSectionStates.ThreePlayer:
                WallSections[0].SetActive(false);
                break;
            case WallSectionStates.TwoPlayer:
                WallSections[1].SetActive(false);

                break;
        }
    }
    private void SpawnAllPlayers()
    {
        for (int i = 0; i < Players.Count; i++)
        {
            Players[i].Instance = Instantiate(PlayerPrefab, Players[i].SpawnPoint.position, Players[i].SpawnPoint.rotation) as GameObject;
            Players[i].PlayerNumber = i + 1;
            Players[i].Setup();
        }
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
        ResetAllPlayers();
        DisablePlayerControl();
        Audio.clip = RoundSounds[0];
        Audio.Play();
        roundNumber++;
        MessageText.text = "ROUND" + roundNumber;
        yield return startWait;
    }

    private IEnumerator RoundPlaying()
    {
        EnablePlayerControl();

        MessageText.text = string.Empty;

        while (!OnePlayerLeft())
        {
            yield return null;

        }
    }

    private IEnumerator RoundEnding()
    {
        DisablePlayerControl();
        Audio.clip = RoundSounds[1];
        Audio.Play();
        roundWinner = null;

        roundWinner = GetRoundWinner();

        if (roundWinner != null)
            roundWinner.Wins++;

        gameWinner = GetGameWinner();

        string message = EndMessage();
        MessageText.text = message;

        yield return endWait;
    }

    private bool OnePlayerLeft()
    {
        int numPlayersLeft = 0;

        for (int i = 0; i < Players.Count; i++)
        {
            if (Players[i].Instance.activeSelf)
                numPlayersLeft++;
        }
        if (numPlayersLeft == 3)
        {
            WallSectionCurrentState = WallSectionStates.ThreePlayer;
            UpdateWallSectionState();
        }
        else if (numPlayersLeft == 2)
        {
            WallSectionCurrentState = WallSectionStates.TwoPlayer;
            UpdateWallSectionState();
        }
        return numPlayersLeft <= 1;
    }


    private string EndMessage()
    {
        string message = "DRAW!";

        if (roundWinner != null)
            message = roundWinner.ColoredPlayerText + " WINS THE ROUND!";

        message += "\n\n\n\n";

        for (int i = 0; i < Players.Count; i++)
        {
            message += Players[i].ColoredPlayerText + ": " + Players[i].Wins + " WINS\n";
        }

        if (gameWinner != null)
        {
            message = gameWinner.ColoredPlayerText + " WINS THE GAME!";
                
        }
        

        return message;
    }

    private PlayerManager GetGameWinner()
    {
        for (int i = 0; i < Players.Count; i++)
        {
            if (Players[i].Wins == RoundsToWin)
                return Players[i];
        }

        return null;
    }

    private PlayerManager GetRoundWinner()
    {
        for (int i = 0; i < Players.Count; i++)
        {
            if (Players[i].Instance.activeSelf)
                return Players[i];
        }

        return null;
    }

    private void DisablePlayerControl()
    {
        for (int i = 0; i < Players.Count; i++)
        {
            Players[i].DisableControl();
        }
    }

    private void EnablePlayerControl()
    {
        for (int i = 0; i < Players.Count; i++)
        {
            Players[i].EnableControl();
        }
    }

    private void ResetAllPlayers()
    {
        for (int i = 0; i < Players.Count; i++)
        {
            Players[i].Reset();
            WallSectionCurrentState = WallSectionStates.FourPlayer;
            UpdateWallSectionState();
        }
    }
}
