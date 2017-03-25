using UnityEngine;
using System.Collections;

public class TankShooting : MonoBehaviour
{
    [SerializeField]
    private Transform shellSpawnPoint;
    [SerializeField]
    private Rigidbody tankShellPrefab;
    [SerializeField]
    private float shellVelocity = 100f;
    [SerializeField]
    private float playerNumber = 1;

    private bool canFire = true;

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetButtonDown("Fire" + playerNumber) && canFire)
        {
            StartCoroutine(HandleFiring());
        }
    }

    private void Fire()
    {
        Rigidbody firedShell = GameObject.Instantiate(tankShellPrefab, shellSpawnPoint.position, shellSpawnPoint.rotation) as Rigidbody;

        firedShell.velocity = shellSpawnPoint.forward * shellVelocity;
    }

    private IEnumerator HandleFiring()
    {
        Fire();
        canFire = false;
        yield return new WaitForSeconds(0.5f);
        canFire = true;
    }
}
