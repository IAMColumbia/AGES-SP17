using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

    [SerializeField]
   public bool isSeeker, isHider, hasBeenTagged, isSpectating;

    [SerializeField]
    float movementSpeed;

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

        if (isSpectating == true)
        {
            isHider = false;
            isSeeker = false;

            Debug.Log(name + "is now Spectating");


        }
    }



}
