using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class KeepingCount : MonoBehaviour {

    public GameObject AssignedPlayer;

    private string CurrentScore;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        CurrentScore = AssignedPlayer.GetComponent<PickUp>().PointsPickedUp.ToString();

        gameObject.GetComponent<Text>().text = CurrentScore;
	
	}
}
