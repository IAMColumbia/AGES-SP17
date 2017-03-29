using UnityEngine;
using System.Collections;

public class Keep : MonoBehaviour {

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this.gameObject);
	}
}
