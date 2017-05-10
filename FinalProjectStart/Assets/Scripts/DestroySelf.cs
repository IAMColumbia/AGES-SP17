using UnityEngine;
using System.Collections;

public class DestroySelf : MonoBehaviour {

	[SerializeField]
	private GameObject explosionPrefab;

	private GameObject explosion;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider obj)
	{
		explosion = Instantiate(explosionPrefab, transform.position, transform.rotation) as GameObject;
		Destroy (this.gameObject);

	}

}
