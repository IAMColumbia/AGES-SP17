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
    private TankLapProgressTracker player1Progress;
    [SerializeField]
    private TankLapProgressTracker player2Progress;
    [SerializeField]
    private TankLapProgressTracker player3Progress;
    [SerializeField]
    private TankLapProgressTracker player4Progress;

    // Use this for initialization
    void Start ()
    {
	
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


}
