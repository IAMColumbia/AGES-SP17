using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AfterActionScript : MonoBehaviour {
    [SerializeField]
    Text playerOneScore;

    [SerializeField]
    Text playerTwoScore;

    public Text winnerText;

    GameObject playerOne;
    GameObject playerTwo;
    GameManager Manager;
    // Use this for initialization
    void Start ()
    {
        playerOne = GameObject.Find("Player1");
        playerTwo = GameObject.Find("Player2");
	}
	
	// Update is called once per frame
	void Update ()
    {
        playerOneScore.text = playerOne.GetComponent<PlayerMagic>().KillCount.ToString();
        playerTwoScore.text = playerTwo.GetComponent<PlayerMagic>().KillCount.ToString();

    }
}
