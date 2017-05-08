using UnityEngine;
using System.Collections;

public class RaycastShoot : MonoBehaviour
{

    public int GunDamage = 1;
    public float FireRate = 0.25f;
    public float WeaponRange = 50f;
    public float HitForce = 100f;
    public Transform GunEnd;

    private Camera fpsCam;
    private WaitForSeconds shotDuration = new WaitForSeconds(.07f);
    private AudioSource gunAudio;
    private DeathandRespawn deathAndRespawn;
    private LineRenderer laserLine;
    private float nextFire;

    void Start()
    {
        laserLine = GetComponent<LineRenderer>();
        gunAudio = GetComponent<AudioSource>();
        fpsCam = GetComponentInParent<Camera>();
        deathAndRespawn = GetComponentInParent<DeathandRespawn>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + FireRate;

            StartCoroutine(ShotEffect());

            Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;

            laserLine.SetPosition(0, GunEnd.position);

            if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, WeaponRange))
            {
                laserLine.SetPosition(1, hit.point);

                ShootableObject box = hit.collider.GetComponent<ShootableObject>();

                if (box != null)
                {
                    box.Damage(GunDamage);
                }

                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * HitForce);
                }
            }
            else
            {
                laserLine.SetPosition(1, rayOrigin + (fpsCam.transform.forward * WeaponRange));
            }
        }
    }

    private IEnumerator ShotEffect()
    {
        if (deathAndRespawn.isPlayerDead == false)
        {
            gunAudio.Play();
        }

        laserLine.enabled = true;
        yield return shotDuration;
        laserLine.enabled = false;
    }
}
