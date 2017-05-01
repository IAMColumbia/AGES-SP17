using UnityEngine;
using System.Collections;

public class DiceSideLogic : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerStay(Collider col)
    {
        //TODO: adjust collider sizes so that only one is touching
        if (col.tag == "Floor")
            Debug.Log(this.name);
        //switch if 
    }

}
