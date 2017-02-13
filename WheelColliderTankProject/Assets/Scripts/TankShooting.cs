﻿using UnityEngine;
using System.Collections;

public class TankShooting : MonoBehaviour 
{
    // This class just handles reading the fire input and firing the tank shell.
    // TODO: we'll want to implement a fire cooldown. Probably use a coroutine?
    [SerializeField]
    ParticleSystem ExplosionParticles;
    [SerializeField]
    Transform ExplosionPoint;
    [Tooltip("Bullet will spawn here. Make sure its collider isn't hitting the same tank that is shooting it!")]
    [SerializeField]
    private Transform shellSpawnPoint;

    [SerializeField]
    private Rigidbody tankShellPrefab;

    [SerializeField]
    private float projectileVelocity = 100;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Fire();
        }
    }

    private void Fire()
    {
        //ParticleSystem explode = Instantiate(ExplosionParticles, ExplosionPoint) as ParticleSystem;
        //Destroy(explode.gameObject, 1.3f);
        
        ExplosionParticles.transform.position = ExplosionPoint.position;
        Rigidbody firedShell = GameObject.Instantiate(tankShellPrefab, shellSpawnPoint.position, shellSpawnPoint.rotation) as Rigidbody;

        firedShell.velocity = shellSpawnPoint.forward * projectileVelocity;
    }    
}
