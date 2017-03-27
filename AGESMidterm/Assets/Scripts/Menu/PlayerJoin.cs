using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerJoin : MonoBehaviour {

    public int PlayerNumber;
    public Button StartGameButton;

    private Text PlayerJoinTexts;
    private GameManager gameManager;

    private void Start()
    {
        PlayerJoinTexts = GetComponent<Text>();
    }
    void Update () {
        if (Input.GetButtonDown("Join" + PlayerNumber))
        {
            PlayerJoinTexts.text = "You're in!";
            StartGameButton.gameObject.SetActive(true);
        }
	}
}
