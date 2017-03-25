using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject winnerPanel;
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
            winnerPanel.SetActive(true);
        }
        else if(player2Progress.hasWon)
        {
            winnerPanel.SetActive(true);
        }
        else if (player3Progress.hasWon)
        {
            winnerPanel.SetActive(true);
        }
        else if (player4Progress.hasWon)
        {
            winnerPanel.SetActive(true);
        }
    }


}
