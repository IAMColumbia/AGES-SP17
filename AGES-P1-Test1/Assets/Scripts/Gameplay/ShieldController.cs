using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShieldController : MonoBehaviour
{
    [SerializeField]
    Rigidbody projectile;

    float bulletSpeed;

    [SerializeField]
    GameObject releasePoint;

    float turnSpeed;

    int absorbedProjectiles;

    float ReleaseRate;

    bool isAbsorbing;

    bool isReleasing;

    public bool isFinishedReleasing;

    int absorbedProjectileLimit;

    void Start()
    {
        bulletSpeed = 3000f;
        turnSpeed = 500f;
        ReleaseRate = 0.5f;
        isAbsorbing = true;
        isReleasing = false;
        isFinishedReleasing = false;
        absorbedProjectiles = 0;
        absorbedProjectileLimit = 10;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet" & isAbsorbing)
        {
            Destroy(collision.gameObject);

            if (absorbedProjectiles < absorbedProjectileLimit)
            {
                absorbedProjectiles++;
            }
        }
    }

    public void BeginRelease(GameObject target)
    {
        StartCoroutine(ReleaseBullets(target));
    }

    IEnumerator ReleaseBullets(GameObject target)
    {
        isAbsorbing = false;
        isReleasing = true;

        while(isReleasing)
        { 
            Vector3 targetDir = target.transform.position - transform.position;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, turnSpeed * Time.deltaTime, 0.0F);
            releasePoint.transform.rotation = Quaternion.LookRotation(newDir);

            Rigidbody clone = (Rigidbody)Instantiate(projectile, releasePoint.transform.position, releasePoint.transform.rotation);
            clone.AddForce(clone.transform.forward * bulletSpeed);

            absorbedProjectiles = absorbedProjectiles - 1;

            yield return new WaitForSeconds(ReleaseRate);

            if (absorbedProjectiles <= 0)
            {
                isReleasing = false;
            }
        }

        isFinishedReleasing = true;
    }
}
