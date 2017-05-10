using UnityEngine;
using System.Collections;

public class Balloon : MonoBehaviour 
{
	[SerializeField] GameManager gameManager;
	[SerializeField] GameObject popParticle;
	[SerializeField] AudioSource audioSource;
	[SerializeField] AudioSource powerupAudioSource;
	[SerializeField] Sprite redBalloon;
	[SerializeField] Sprite metalBalloon;

	private bool isPopped = false;
	private bool isPoppable = true;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		PopBalloon ();
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.tag == "Knife" && isPoppable) 
		{
			if (this.gameObject.name == "Balloon_BlackRoomba") 
			{
				gameManager.balloonList.RemoveAt(1);
				isPopped = true;
			} 
			else if (this.gameObject.name == "Balloon_SilverRoomba") 
			{
				gameManager.balloonList.RemoveAt(0);
				isPopped = true;
			}
		}
	}

	void PopBalloon()
	{
		if (isPopped == true && GetComponent<SpriteRenderer>().enabled == true && isPoppable) 
		{
			Instantiate (popParticle, transform.position, transform.rotation);
			audioSource.Play ();
			GetComponent<SpriteRenderer> ().enabled = false;
			GetComponent<PolygonCollider2D> ().enabled = false;
			if (!audioSource.isPlaying) 
			{
				Destroy (gameObject);
			}
		}
	}

	public IEnumerator balloonPowerup()
	{
		isPoppable = false;
		GetComponent<SpriteRenderer> ().sprite = metalBalloon;
		powerupAudioSource.Play ();

		yield return new WaitForSeconds (gameManager.powerupTime);

		isPoppable = true;
		GetComponent<SpriteRenderer> ().sprite = redBalloon;
	}
}
