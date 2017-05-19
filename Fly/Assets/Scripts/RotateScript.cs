using UnityEngine;
using System.Collections;

public class RotateScript : MonoBehaviour {

	// Use this for initialization
	
	// Update is called once per frame
	void Update () {
        Rotation();
    }
    void Rotation()
    {
        transform.Rotate(0, 100 * Time.deltaTime, 0);
    }
}
