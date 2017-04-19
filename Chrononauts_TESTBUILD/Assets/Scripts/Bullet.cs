using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    [SerializeField]
    int bulletSpeed = 5;
    [SerializeField]
    string enemyTag;

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

    void OnCollisionEnter2D(Collider2D other)
    {
        if (other.tag == enemyTag)
        {
            other.GetComponent<GameObject>().SetActive(false);
        }
    }
}
