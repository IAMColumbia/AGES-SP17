using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Invoke("Kill", 2f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Kill()
    {
        Destroy(gameObject);
    }

    
}
