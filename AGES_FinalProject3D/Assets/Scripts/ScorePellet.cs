using UnityEngine;
using System.Collections;

public class ScorePellet : MonoBehaviour
{
    private GameUI gameUI;

    private const int scoreValue = 10;
    private bool hasBeenPickedUp;
    private MeshRenderer scorePelletMesh;
    private AudioSource pickupSound;
    private SphereCollider trigger;

	// Use this for initialization
	void Start ()
    {
        hasBeenPickedUp = false;
        gameUI = GameObject.Find("GameUI").GetComponent<GameUI>();
        scorePelletMesh = gameObject.GetComponent<MeshRenderer>();
        pickupSound = gameObject.GetComponent<AudioSource>();
        trigger = gameObject.GetComponent<SphereCollider>();
	}

    private void OnTriggerEnter(Collider other)
    {
        trigger.enabled = false;
        if (other.gameObject.tag == "Player" && hasBeenPickedUp == false)
        {
            hasBeenPickedUp = true;
            gameUI.UpdateScoreText(scoreValue);
            scorePelletMesh.enabled = false;
            pickupSound.Play();
        }
    }
}
