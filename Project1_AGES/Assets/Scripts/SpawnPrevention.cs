using UnityEngine;
using System.Collections;

public class SpawnPrevention : MonoBehaviour {

	public GameObject point;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnTriggerEnter(Collider obj)
	{
		if (obj.tag == "Point") 
		{
			Destroy (obj.gameObject);
			Vector3 position = new Vector3 (Random.Range (-12f, 12f), 5f, Random.Range (-12f, 12f));
			Instantiate (point, position, Quaternion.identity);
		}
	}

//	public void OnTriggerStay(Collider obj)
//	{
//		if (obj.tag == "Point") 
//		{
//			Destroy (obj.gameObject);
//			Vector3 position = new Vector3 (Random.Range (-12f, 12f), 5f, Random.Range (-12f, 12f));
//			Instantiate (point, position, Quaternion.identity);
//		}
//	}
}
