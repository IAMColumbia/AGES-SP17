using UnityEngine;
using System.Collections;

public class PickUp : MonoBehaviour {

	public int PointsPickedUp;
	public GameObject point;

	// Use this for initialization
	void Start () {
	
		PointsPickedUp = 0;
		Vector3 position = new Vector3 (Random.Range(-12f,12f), 0.34f, Random.Range(-12f,12f));
		Instantiate (point, position, Quaternion.identity);

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnTriggerEnter(Collider obj)
	{
		if(obj.tag == "Point")
		{
			PointsPickedUp += 1;
			Destroy (obj.gameObject);
			Vector3 position = new Vector3 (Random.Range(-12f,12f), 0.34f, Random.Range(-12f,12f));
			Instantiate (point, position, Quaternion.identity);
		}

		if(obj.tag == "PowerUp")
		{
			Destroy (obj.gameObject);
		}
	}
}
