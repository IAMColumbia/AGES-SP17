using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    Image gameEndPanel;
    [SerializeField]
    GameObject youWinText;

    public Transform currentCheckpoint;
    public Transform currentCheckpointMirror;
    public bool ReachedGoal = false;

	// Use this for initialization
	void Start ()
    {
        gameEndPanel.canvasRenderer.SetAlpha(0);
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(ReachedGoal == true)
        {
            HandleWinning();
        }
	
	}

    private void HandleWinning()
    {
        gameEndPanel.CrossFadeAlpha(1, 1, false);
        youWinText.SetActive(true);
    }
}
