using UnityEngine;
using System.Collections;

public class Dust : MonoBehaviour 
{
	[SerializeField] float dustDeleteTime = 10;

	private float timer;
	private GameObject gameManager;
	private GameManager gameManagerScript;
	private AudioSource audioSource;

	// Use this for initialization
	void Start () 
	{
		timer = Time.time + dustDeleteTime;
		gameManager = GameObject.Find ("Game Manager");
		gameManagerScript = gameManager.GetComponent<GameManager> ();
		audioSource = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		DeleteDust ();
	}

	void DeleteDust()
	{
		if (timer < Time.time) 
		{
			Destroy (this.gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Black Roomba") 
		{
			gameManagerScript.blackRoombaDust++;
			audioSource.Play ();
			GetComponent<SpriteRenderer> ().enabled = false;
			GetComponent<CircleCollider2D> ().enabled = false;
			Destroy (this.gameObject, audioSource.clip.length);
		} 
		else if (other.gameObject.tag == "Silver Roomba") 
		{
			gameManagerScript.silverRoombaDust++;
			audioSource.Play ();
			GetComponent<SpriteRenderer> ().enabled = false;
			GetComponent<CircleCollider2D> ().enabled = false;
			Destroy (this.gameObject, audioSource.clip.length);
		}
	}
}
