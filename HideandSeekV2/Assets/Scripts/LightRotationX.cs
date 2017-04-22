using UnityEngine;
using System.Collections;

public class LightRotationX : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.Rotate(new Vector3(.5f, 0, 0));
    }
}
