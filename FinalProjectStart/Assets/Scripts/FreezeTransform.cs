using UnityEngine;
using System.Collections;

public class FreezeTransform : MonoBehaviour {

    private Quaternion startingRotation;

	// Use this for initialization
	void Start () {

        startingRotation = this.transform.rotation;
	
	}
	
	// Update is called once per frame
	void Update () {

        this.transform.rotation = startingRotation;
	
	}
}
