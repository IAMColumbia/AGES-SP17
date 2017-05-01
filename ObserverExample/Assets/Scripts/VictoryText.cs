using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VictoryText : MonoBehaviour 
{
    private Text text;

	// Use this for initialization
	void Start () 
	{
        text = GetComponent<Text>();

        // clear the text 
        text.text = string.Empty;
	}

    private void PlayerWonEventHandler(string message)
    {
        text.text = message;
    }
	
    private void OnEnable()
    {
        GameManager.PlayerWonEvent += PlayerWonEventHandler;
    }

    private void OnDisable()
    {
        GameManager.PlayerWonEvent -= PlayerWonEventHandler;
    }
}
