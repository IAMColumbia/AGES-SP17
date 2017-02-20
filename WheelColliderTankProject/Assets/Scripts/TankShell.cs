using UnityEngine;
using System.Collections;

public class TankShell : MonoBehaviour 
{
    // This class dictates the behavior of the tank shell.
    // It explodes when it hits something and should do damage to IDamageables.

    // It needs to go on the COLLIDER game object, which is a child of the tank shell model.
    // The collider had to go on a child object or the prefab wouldn't save the collider's shape settings...
    // That might be an issue with ProBuilder, or maybe just a Unity thing.

    [SerializeField]
    private float maxLifetime = 2;

    [SerializeField]
    private float explosionRadius = 10;

    [Tooltip("Don't make this too high or it will look bad on light to average mass rigidbodies. There is a special interface for Heavy Explodable Objects.")]
    [SerializeField]
    private float explosionForce = 1000;

    [SerializeField]
    private LayerMask layersToAffect;

    [SerializeField]
    private ParticleSystem ExplosionParticle;

    private Rigidbody rigidbody_useThis;
    


	private void Start () 
	{
        Destroy(transform.parent.gameObject, maxLifetime);
        rigidbody_useThis = GetComponentInParent<Rigidbody>();
	}

    private void OnTriggerEnter(Collider other)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius, layersToAffect);
        
        for (int i = 0; i < colliders.Length; i++)
        {
            Rigidbody targetRigidbody = colliders[i].GetComponent<Rigidbody>();

            // If they don't have a rigidbody, go on to the next collider.
            if (!targetRigidbody)
                continue;

            Debug.Log("Shell hit: " + targetRigidbody.gameObject.name);

            targetRigidbody.AddExplosionForce(explosionForce, transform.position, explosionRadius);

            // Special behavior for heavy things, because otherwise they doesn't move in a very satisfying way when hit.
            IHeavyExplodableObject heavyObject = targetRigidbody.GetComponentInParent<IHeavyExplodableObject>();


            if (heavyObject != null)
            {
                heavyObject.Explode(rigidbody_useThis.velocity.normalized);
            }

            TankHealth targetHealth = targetRigidbody.GetComponent<TankHealth>();
            
            targetHealth.TakeDamage();
        }

            ExplosionParticle.transform.parent = null;
            ExplosionParticle.Play();

            Destroy(ExplosionParticle.gameObject, ExplosionParticle.duration);
            Destroy(gameObject);
    }
    
}
