using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

	[SerializeField]
	private float expandRate;

	[SerializeField]
	private float lifeSpan;

	// Use this for initialization
	void Start () {

		Destroy (this.gameObject, lifeSpan);
	
	}
	
	// Update is called once per frame
	void Update () {

		transform.localScale = transform.localScale * expandRate;
	
	}
}
