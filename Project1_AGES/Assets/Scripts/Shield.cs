using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour {

    [SerializeField]
    private GameObject playersShield;



	// Use this for initialization
	void Start () {

        playersShield.SetActive(false);
	
	}
	
	// Update is called once per frame
	void Update () {

        if(Input.GetButtonDown("Shield"))
        {
            if (!Input.GetButton("Attack"))
            {
                playersShield.SetActive(true);
            }
            
        }
        if (Input.GetButtonUp("Shield"))
        {
            playersShield.SetActive(false);
        }
	
	}
}
