using UnityEngine;
using System.Collections;

public class Dash : MonoBehaviour {

    [SerializeField]
    private GameObject dashLoc;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 possibleDestination = new Vector3(dashLoc.transform.position.x, dashLoc.transform.position.y, dashLoc.transform.position.z);

        if (Input.GetButtonDown("Dash"))
        {
            transform.Translate(possibleDestination);
        }
	
	}
}
