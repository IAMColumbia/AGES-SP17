using UnityEngine;
using System.Collections;
using System;

public class Enemy : Character, IDamageable {

    public bool Alive { get { return alive; } private set { alive = value; } }
    bool alive = true;
    public float MaxHealth = 100;

    public Material positiveMaterial, negativeMaterial, damageMaterial;

    [SerializeField]
    HalfEnemy Aside, Bside;

    [SerializeField]
    Bullet bullet;

    [SerializeField]
    public Transform[] firingPositions;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	    if(!Aside.Alive && !Bside.Alive)
        {
            Die();
        }
	}

    void Die()
    {
        Destroy(this.gameObject);
    }

    public void Shoot(Transform firingPosition)
    {
        Bullet newBulletA = Instantiate<Bullet>(bullet);
        Bullet newBulletB = Instantiate<Bullet>(bullet);
        newBulletA.transform.position = firingPosition.position;
        newBulletB.transform.position = firingPosition.position;
        newBulletA.Init(Aside.Alive, Aside.gameObject.layer, firingPosition.forward * 2, this);
        newBulletB.Init(Bside.Alive, Bside.gameObject.layer, firingPosition.forward * 2, this);
    }

    public void ShootAll()
    {
        foreach (Transform firingPosition in firingPositions)
        {
            Shoot(firingPosition);
        }
    }

    public void takeDamage(Bullet bullet)
    {
        
    }
}
