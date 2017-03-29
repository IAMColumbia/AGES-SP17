using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheaterTrigger : MonoBehaviour {

    public bool playerOneHasPassed = false;
    public bool playerTwoHasPassed = false;

	// Use this for initialization
	void Start ()
    {
        MeshRenderer meshRender = this.gameObject.GetComponent<MeshRenderer>();
        //Make self invisible
        meshRender.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerOneHasPassed = true;
            //Debug.Log("Player 1 has passed.");
        }
        if (other.tag == "Player2")
        {
            playerTwoHasPassed = true;
            //Debug.Log("Player 2 has passed.");
        }

    }
}
