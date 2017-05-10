using UnityEngine;
using System.Collections;

public class Fire : MonoBehaviour {


	[SerializeField]
	private GameObject projectilePrefab;

	[SerializeField]
	private string playerNumber;

	[SerializeField]
	private float speed;

	private GameObject projectile;

	public int Ammo;

	// Use this for initialization
	void Start () {

		Ammo = 0;
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetButtonDown("Fire" + playerNumber) && Ammo >= 1) 
		{
			Ammo--;

			projectile = Instantiate(projectilePrefab, transform.position, transform.rotation) as GameObject;
			Rigidbody rb = projectile.GetComponent<Rigidbody> ();
			rb.velocity = transform.forward * speed;
		}
	
	}
}
