using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour 
{
	public int blackRoombaDust;
	public int silverRoombaDust;
	public float powerupTime = 10f;
	public List<GameObject> balloonList = new List<GameObject>();

	[SerializeField] float dustSpawnTime = 3f;
	[SerializeField] GameObject canvas;
	[SerializeField] RoombaMovement blackRoombaScript;
	[SerializeField] RoombaMovement silverRoombaScript;
	[SerializeField] AudioSource blackRoombaAudioSource;
	[SerializeField] AudioSource silverRoombaAudioSource;
	[SerializeField] GameObject dustPrefab;

	float xMin = -11;
	float xMax = 11;
	float yMin = -6;
	float yMax = 6;
	private GameObject lastBalloon;
	private Text winText;
	private float winDelay;
	private float timer;

	// Use this for initialization
	void Start () 
	{
		timer = Time.time + dustSpawnTime;

		winText = canvas.GetComponentInChildren<Text> ();

		foreach (GameObject balloon in GameObject.FindGameObjectsWithTag("Balloon")) 
		{
			balloonList.Add (balloon);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		WinScreen ();
		PlayRoombaSound ();
		SpawnDust ();
		DoBalloonPowerup ();
	}

	void WinScreen()
	{
		if (balloonList.Count <= 1) 
		{
			StartCoroutine (WinCanvasDelay ());
		}
	}

	public void ResetLevel()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}

	void PlayRoombaSound()
	{
		if (!blackRoombaScript.isMoving == true) 
		{
			blackRoombaAudioSource.Play ();
		}
		if (!silverRoombaScript.isMoving == true) 
		{
			silverRoombaAudioSource.Play ();
		}
	}

	IEnumerator WinCanvasDelay()
	{
		yield return new WaitForSeconds (1);

		//lastBalloon = GameObject.FindGameObjectWithTag ("Balloon");
		lastBalloon = balloonList [0];
		canvas.SetActive (true);

		if (lastBalloon.gameObject.name == "Balloon_BlackRoomba") 
		{
			winText.text = "Black Roomba Wins!";
		}
		else if(lastBalloon.gameObject.name == "Balloon_SilverRoomba")
		{
			winText.text = "Silver Roomba Wins!";
		}
	}

	void SpawnDust()
	{
		Vector2 pos = new Vector2 (Random.Range (xMin, xMax), Random.Range(yMin, yMax));

		if (timer < Time.time) 
		{
			Instantiate (dustPrefab, pos, transform.rotation);
			timer = Time.time + dustSpawnTime;
		}
	}

	void DoBalloonPowerup()
	{
		if (blackRoombaDust >= 3) 
		{
			GameObject blackBalloon = GameObject.Find ("Balloon_BlackRoomba");
			Balloon blackBalloonScript = blackBalloon.GetComponent<Balloon> ();

			StartCoroutine (blackBalloonScript.balloonPowerup ());
			blackRoombaDust = 0;
		} 
		else if (silverRoombaDust >= 3) 
		{
			GameObject silverBalloon = GameObject.Find ("Balloon_SilverRoomba");
			Balloon silverBalloonScript = silverBalloon.GetComponent<Balloon> ();

			StartCoroutine (silverBalloonScript.balloonPowerup ());
			silverRoombaDust = 0;
		}
	}
}
