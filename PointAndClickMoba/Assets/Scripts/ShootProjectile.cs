using UnityEngine;
using System.Collections;

public class ShootProjectile : MonoBehaviour
{
    [SerializeField]
    float speed = 2;
    [SerializeField]
    float explosionRadius = 10;
    [SerializeField]
    float travelDistance = 10;
    [SerializeField]
    GameObject explosion;
    [SerializeField]
    float damage;

    Vector3 startPosition;
    GameObject explosionObject;

    void Awake()
    {
        startPosition = transform.position;
        GetComponent<Rigidbody>().AddForce(transform.parent.forward * speed);
        transform.parent = null;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, startPosition) >= travelDistance)
        {
            Explode();
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<TakeDamage>())
        {
            Explode();
        }
    }

    void Explode()
    {
        RaycastHit[] explosionRayHit = Physics.SphereCastAll(transform.position, explosionRadius, Vector3.forward, 0);
        explosionObject = Instantiate(explosion, transform.position, transform.rotation, null) as GameObject;

        foreach (var objectHit in explosionRayHit)
        {
            if (objectHit.transform.GetComponent<TakeDamage>())
            {
                objectHit.transform.GetComponent<TakeDamage>().Damaged(damage);
            }
        }

        Destroy(gameObject);
    }

    void SetObjectInactive()
    {
        if (explosion != null)
        {
            explosion.SetActive(false);
        }
    }
}
