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
    float damageToDeal = 25;

    [SerializeField]
    private float maxLifetime = 2;

    [SerializeField]
    private float explosionRadius = 10;

    [Tooltip("Don't make this too high or it will look bad on light to average mass rigidbodies. There is a special interface for Heavy Explodable Objects.")]
    [SerializeField]
    private float explosionForce = 2000;

    [SerializeField]
    private LayerMask layersToAffect;

    private Rigidbody rigidbody_useThis;

	// Use this for initialization
	private void Start () 
	{
        // Failsafe incase the bullet doesn't hit anything, destroy it after a while to make sure it goes away.
        Destroy(transform.parent.gameObject, maxLifetime);
        rigidbody_useThis = GetComponentInParent<Rigidbody>();
	}

    private void OnTriggerEnter(Collider other)
    {
        // Collect all the colliders in a sphere from the shell's current position to a radius of the explosion radius.
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius, layersToAffect);

        // Go through all the colliders...
        for (int i = 0; i < colliders.Length; i++)
        {

            // ... and find their rigidbody.
            Rigidbody targetRigidbody = colliders[i].GetComponent<Rigidbody>();


            // I'm leaving this code in as an example of continue; but I don't think
            // its the best way to organize this.

            // If they don't have a rigidbody, go on to the next collider.
            if (!targetRigidbody)
                continue;

            Debug.Log("Shell hit: " + targetRigidbody.gameObject.name);

            // Add an explosion force. This is fine for most light to average mass rigidbodies.
            // Don't make it too high though or lighter objects go way too fast and it doesn't look good.
            targetRigidbody.AddExplosionForce(explosionForce, transform.position, explosionRadius);
          
            // Special behavior for heavy things, because otherwise they doesn't move in a very satisfying way when hit.
            IHeavyExplodableObject heavyObject = targetRigidbody.GetComponentInParent<IHeavyExplodableObject>();

            if (heavyObject != null)
            {
                heavyObject.Explode(rigidbody_useThis.velocity.normalized);
            }

            IDamageable damageableObject = targetRigidbody.GetComponentInParent<IDamageable>();

            if (damageableObject != null)
            {
                damageableObject.TakeDamage(damageToDeal, Time.time);
            }

        }

        // TODO: Implement explosion VFX! See the TANKS! Unity tutorial for a perfect example.

        // Destroy the shell, since it exploded
        Destroy(transform.parent.gameObject);
    }
    
}
