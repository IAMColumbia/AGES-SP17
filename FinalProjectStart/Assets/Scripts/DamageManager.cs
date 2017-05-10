using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class DamageManager : MonoBehaviour {

	[SerializeField]
	private GameObject player;

	[SerializeField]
	private GameObject RoundOverScreen;

	[SerializeField]
	private GameObject InGameScreen;

	[SerializeField]
	private GameObject winnerText;

	[SerializeField]
	private int health;

	[SerializeField]
	public int playerNumber;

	private Text actualText;

	// Use this for initialization
	void Start () {

		health = 1;
		RoundOverScreen.SetActive(false);
		InGameScreen.SetActive (true);
	
	}
	
	// Update is called once per frame
	void Update () {

		CheckStatus ();
	
	}

	void OnTriggerEnter(Collider obj)
	{
		if(obj.tag == "Explosion")
		{
			health--;
		}
	}

	private void CheckStatus()
	{
		if(health == 0)
		{
			Destroy (player);
			RoundOverScreen.SetActive(true);
			InGameScreen.SetActive (false);

			actualText =  winnerText.GetComponent <Text> () as Text;
			actualText.text = "PLAYER " + playerNumber.ToString ();

		}
	}
}
