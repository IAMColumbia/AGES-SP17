using UnityEngine;
using System.Collections;

public class PickUp : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnTriggerEnter(Collider obj)
	{
		if(obj.tag == "Point")
		{
			Destroy (obj.gameObject);
		}

		if(obj.tag == "PowerUp")
		{
			Destroy (obj.gameObject);
		}
	}
}
