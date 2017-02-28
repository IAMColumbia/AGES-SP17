using UnityEngine;
using System.Collections;

public class MaxLifeTimer : MonoBehaviour {
    float maxLifetime = 2.0f;
	// Use this for initialization
	void Start () {

        Destroy(gameObject, maxLifetime);
    }
	
}
