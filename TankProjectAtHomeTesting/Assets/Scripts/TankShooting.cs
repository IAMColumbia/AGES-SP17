﻿using UnityEngine;
using System.Collections;

public class TankShooting : MonoBehaviour 
{
    // This class just handles reading the fire input and firing the tank shell.
    // TODO: we'll want to implement a fire cooldown. Probably use a coroutine?

    [Tooltip("Bullet will spawn here. Make sure its collider isn't hitting the same tank that is shooting it!")]
    [SerializeField]
    private Transform shellSpawnPoint;

    [SerializeField]
    private Rigidbody tankShellPrefab;

    [SerializeField]
    private float projectileVelocity = 100;

    [Tooltip("We're going to have a backwards force on the turret when the tank shoots to simulate recoil.")]
    [SerializeField]
    Rigidbody turretRigidBody;

    [SerializeField]
    float recoilForce = 100000;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Fire();
        }
    }

    private void Fire()
    {
        Rigidbody firedShell = GameObject.Instantiate(tankShellPrefab, shellSpawnPoint.position, shellSpawnPoint.rotation) as Rigidbody;

        firedShell.velocity = shellSpawnPoint.forward * projectileVelocity;

        turretRigidBody.AddForce(turretRigidBody.transform.forward * -recoilForce);
    }    
}
