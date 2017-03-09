using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour {



	// Use this for initialization
	void Start () {

        //this.transform.rotation = new Quaternion(0,0,0,0);
	
	}
	
	// Update is called once per frame
	void Update () {

        if(Input.GetButtonDown("Attack"))
        {
            this.transform.Rotate(90, 0, 0);
        }
        if (Input.GetButtonUp("Attack"))
        {
            this.transform.Rotate(-90, 0, 0);
        }


    }
}
