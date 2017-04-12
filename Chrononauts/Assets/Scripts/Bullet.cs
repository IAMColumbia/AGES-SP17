using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    [SerializeField]
    GameObject spawner;

    [SerializeField]
    int bulletSpeed;

    Rigidbody2D bulletHull;



	// Use this for initialization
	void Start ()
    {
        //Vector2 bulletDirection = new Vector2(spawner.transform.position.x, spawner.transform.position.y);
        bulletHull = this.gameObject.GetComponent<Rigidbody2D>();
        //bulletHull.AddForce(bulletDirection * bulletSpeed, ForceMode2D.Impulse);
        bulletHull.AddForce(this.transform.up * bulletSpeed, ForceMode2D.Impulse);

    }

    // Update is called once per frame
    void Update ()
    {
		
	}
}
