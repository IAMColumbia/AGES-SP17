using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TankLapProgressTracker : MonoBehaviour
{
    [SerializeField]
    private GameObject victoryCameraToActivate;
    [SerializeField]
    private GameObject cameraToDeactivate;
    [SerializeField]
    private Collider finishLineCollider;
    [SerializeField]
    private Text playerLapText;

    public bool hasPassedCheckpoint1;
    public bool hasPassedCheckpoint2;
    public bool hasPassedCheckpoint3;
    public bool hasPassedCheckpoint4;
    public int currentLap = 1;
    public bool hasWon = false;
	
	// Update is called once per frame
	void Update ()
    {
        handleWinning();
        playerLapText.text = "Laps: " + currentLap + "/3";

    }

    private void handleWinning()
    {
        if(currentLap == 4)
        {
            hasWon = true;
            cameraToDeactivate.SetActive(false);
            victoryCameraToActivate.SetActive(true);
            finishLineCollider.enabled = false;
        }
    }
}
