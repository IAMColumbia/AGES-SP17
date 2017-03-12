using UnityEngine;
using System.Collections;

public class DelayedDamage : MonoBehaviour
{
    [SerializeField]
    float hitDelay = 0.5f;
    [SerializeField]
    float damageValue = 10;
    [SerializeField]
    GameObject lazerHit;

    GameObject lazerHitObject;

    void Awake()
    {
        GetComponent<ParticleSystem>().Play();
        Invoke("CastRay", hitDelay);
    }

    void CastRay()
    {
        GetComponent<ParticleSystem>().Stop();

        RaycastHit[] lazerRayHit = Physics.SphereCastAll(transform.parent.position, GetComponent<ParticleSystem>().shape.box.x / 2, transform.parent.forward, GetComponent<ParticleSystem>().shape.box.z);

        foreach (var objectHit in lazerRayHit)
        {
            if (objectHit.transform.GetComponent<TakeDamage>())
            {
                objectHit.transform.GetComponent<TakeDamage>().Damaged(damageValue);
            }
        }

        lazerHitObject = Instantiate(lazerHit, transform.position, transform.rotation) as GameObject;

        Destroy(gameObject);
    }
}
