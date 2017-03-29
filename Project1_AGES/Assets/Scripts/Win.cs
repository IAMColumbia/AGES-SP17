using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour {

    [SerializeField]
    private int gameScene;
    [SerializeField]
    private int menuScene;

    [SerializeField]
    private GameObject p1;
    [SerializeField]
    private GameObject p2;
    [SerializeField]
    private GameObject p3;
    [SerializeField]
    private GameObject p4;

    public int p1PointCount;
    public int p2PointCount;
    public int p3PointCount;
    public int p4PointCount;

    private string winnerPlayer;

    // Use this for initialization
    void Start () {

        p1PointCount = p1.GetComponentInChildren<PickUp>().PointsPickedUp;
        p2PointCount = p2.GetComponentInChildren<PickUp>().PointsPickedUp;
        p3PointCount = p3.GetComponentInChildren<PickUp>().PointsPickedUp;
        p4PointCount = p4.GetComponentInChildren<PickUp>().PointsPickedUp;

        WinCalc();

        gameObject.GetComponent<Text>().text = winnerPlayer;

    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButtonDown("Start"))
        {
            SceneManager.LoadScene(gameScene);
        }

        if (Input.GetButtonDown("Exit"))
        {
            SceneManager.LoadScene(menuScene);
        }

    }

    private void WinCalc()
    {
        if (p1PointCount > p2PointCount && p1PointCount > p3PointCount && p1PointCount > p4PointCount)
        {
            winnerPlayer = "PLAYER 1";
        }

        else if (p2PointCount > p1PointCount && p2PointCount > p3PointCount && p2PointCount > p4PointCount)
        {
            winnerPlayer = "PLAYER 2";
        }

        else if (p3PointCount > p1PointCount && p3PointCount > p2PointCount && p3PointCount > p4PointCount)
        {
            winnerPlayer = "PLAYER 3";
        }

        else if (p4PointCount > p1PointCount && p4PointCount > p3PointCount && p4PointCount > p2PointCount)
        {
            winnerPlayer = "PLAYER 4";
        }

        else
        {
            winnerPlayer = "NO ONE";
        }
    }
}
