using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

    [SerializeField]
    bool isSeeker, isHider, hasBeenTagged, isSpectating;


    void Start ()
    {

    }
	
	// Update is called once per frame
	void Update () {
        if (isSeeker == true)
        {
            this.gameObject.tag = "Seeker";

        }

        if (isHider == true)
        {
            this.gameObject.tag = "Hider";

        }
    }



}
