using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject winnerPanel;
    [SerializeField]
    private GameObject raceUIPanel;
    [SerializeField]
    private Text readyGoText;
    [SerializeField]
    private int numberofPlayers;
    [SerializeField]
    private TankLapProgressTracker player1Progress;
    [SerializeField]
    private TankLapProgressTracker player2Progress;
    [SerializeField]
    private TankLapProgressTracker player3Progress;
    [SerializeField]
    private TankLapProgressTracker player4Progress;
    [SerializeField]
    private TankMovement player1Movement;
    [SerializeField]
    private TankMovement player2Movement;
    [SerializeField]
    private TankMovement player3Movement;
    [SerializeField]
    private TankMovement player4Movement;
    [SerializeField]
    private TankShooting player1Shoot;
    [SerializeField]
    private TankShooting player2Shoot;
    [SerializeField]
    private TankShooting player3Shoot;
    [SerializeField]
    private TankShooting player4Shoot;

    // Use this for initialization
    void Start ()
    {
        StartCoroutine(StartRaceDelay());
    }
	
	// Update is called once per frame
	void Update ()
    {
        
        ShowWinningPaneltoAppropriateWinner();
	}

    private void ShowWinningPaneltoAppropriateWinner()
    {
        if(player1Progress.hasWon)
        {
            raceUIPanel.SetActive(false);
            winnerPanel.SetActive(true);
        }
        else if(player2Progress.hasWon)
        {
            raceUIPanel.SetActive(false);
            winnerPanel.SetActive(true);
        }
        else if (player3Progress.hasWon)
        {
            raceUIPanel.SetActive(false);
            winnerPanel.SetActive(true);
        }
        else if (player4Progress.hasWon)
        {
            raceUIPanel.SetActive(false);
            winnerPanel.SetActive(true);
        }
    }

    private IEnumerator StartRaceDelay()
    {
        DisableMovementAndShooting();
        readyGoText.text = "Ready...";
        yield return new WaitForSeconds(3f);
        readyGoText.text = "Go!!!";
        EnableMovementAndShooting();
        yield return new WaitForSeconds(.5f);
        readyGoText.text = "";
    }

    private void DisableMovementAndShooting()
    {
        player1Movement.enabled = false;
        player2Movement.enabled = false;
        player3Movement.enabled = false;
        player4Movement.enabled = false;

        player1Shoot.enabled = false;
        player2Shoot.enabled = false;
        player3Shoot.enabled = false;
        player4Shoot.enabled = false;
    }

    private void EnableMovementAndShooting()
    {
        if(numberofPlayers == 2)
        {
            player1Movement.enabled = true;
            player2Movement.enabled = true;

            player1Shoot.enabled = true;
            player2Shoot.enabled = true;
        }
        else if(numberofPlayers == 3)
        {
            player1Movement.enabled = true;
            player2Movement.enabled = true;
            player3Movement.enabled = true;

            player1Shoot.enabled = true;
            player2Shoot.enabled = true;
            player3Shoot.enabled = true;
        }
        else if(numberofPlayers == 4)
        {
            player1Movement.enabled = true;
            player2Movement.enabled = true;
            player3Movement.enabled = true;
            player4Movement.enabled = true;

            player1Shoot.enabled = true;
            player2Shoot.enabled = true;
            player3Shoot.enabled = true;
            player4Shoot.enabled = true;
        }
    }


}
