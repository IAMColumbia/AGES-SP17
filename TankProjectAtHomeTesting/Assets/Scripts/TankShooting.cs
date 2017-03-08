using UnityEngine;
using System.Collections;

public class TankShooting : MonoBehaviour 
{
    // This class just handles reading the fire input and firing the tank shell.
    [SerializeField]
    float shootCooldown = 1.5f;

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

    private TankController tankController;
    private bool canShoot = true;

    private void Start()
    {
        tankController = GetComponent<TankController>();
    }

    private void Update()
    {
        if (tankController.TankCanBeControlled && canShoot)
        {
            if (Input.GetButtonDown("Fire1P" + tankController.ControllingPlayer.PlayerNumber))
            {
                Fire();
            }
        }
    }

    private void Fire()
    {
        canShoot = false;
        StartCoroutine(ResetCanShootAfterCooldown());
        Rigidbody firedShell = GameObject.Instantiate(tankShellPrefab, shellSpawnPoint.position, shellSpawnPoint.rotation) as Rigidbody;

        firedShell.velocity = shellSpawnPoint.forward * projectileVelocity;

        // TankShell script is on a collier child object so that onTriggerEnter works.
        // The collider must be on a child because otherwise it's custom settings don't save with the prefab.
        // I think this might be a probuilder issue.
        firedShell.GetComponentInChildren<TankShell>().ControllingPlayer = tankController.ControllingPlayer;

        turretRigidBody.AddForce(turretRigidBody.transform.forward * -recoilForce);
    }    

    private IEnumerator ResetCanShootAfterCooldown()
    {
        yield return new WaitForSeconds(shootCooldown);

        canShoot = true;
    }
}
