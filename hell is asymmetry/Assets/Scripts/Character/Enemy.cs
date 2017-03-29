using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public enum Event
{
    enemyDied,
    waveEnded,
    hitEnemy,
    killedEnemy,
    firedBullet,
    playerDied
}

public interface Observer
{
    void Notify(Subject sender, Event e);
}

public interface Subject
{
    void Subscribe(Observer o);
}

public class Enemy : Character, IDamageable, Subject {

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

    [SerializeField]
    bool linked = false; //if true, both A and B die simultaneously, if false, one can be killed and the other will remain alive

    List<Observer> observers = new List<Observer>();

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (linked)
        {
            if(!Aside.Alive || !Bside.Alive)
            {
                Die();
            }
        }
        else
        {
	        if(!Aside.Alive && !Bside.Alive)
            {
                Die();
            }
        }
	}

    public void Subscribe(Observer o)
    {
        observers.Add(o);
    }

    void Die(bool explode = true)
    {
        foreach(Observer o in observers)
        {
            o.Notify(this, Event.enemyDied);
        }

        if (explode)
        {
            if (Aside.Alive)
            {
                Aside.takeDamage(9999);
            }
            if (Bside.Alive)
            {
                Bside.takeDamage(9999);
            }
        }

        Destroy(this.gameObject);
    }

    public void MovementComplete()
    {
        Die(explode: false);
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
