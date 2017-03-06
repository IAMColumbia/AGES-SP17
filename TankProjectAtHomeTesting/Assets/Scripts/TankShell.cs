using UnityEngine;
using System.Collections;
using System;

public class TankShell : MonoBehaviour, IDamageSource
{
    // This class dictates the behavior of the tank shell.
    // It explodes when it hits something and should do damage to IDamageables.

    // It needs to go on the COLLIDER game object, which is a child of the tank shell model.
    // The collider had to go on a child object or the prefab wouldn't save the collider's shape settings...
    // That might be an issue with ProBuilder, or maybe just a Unity thing.

    [SerializeField]
    float maxDamageToDeal = 25;

    [SerializeField]
    float minimumDamageToDeal = 5;

    [SerializeField]
    private float maxLifetime = 2;

    [SerializeField]
    private float explosionRadius = 10;

    [Tooltip("Don't make this too high or it will look bad on light to average mass rigidbodies. There is a special interface for Heavy Explodable Objects.")]
    [SerializeField]
    private float explosionForce = 2000;

    [SerializeField]
    private LayerMask layersToAffect;

    [SerializeField]
    private GameObject particleGameObject;

    private Rigidbody rigidbody_useThis;

    public Player ControllingPlayer
    { get; set; }

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
            // its the best way to organize this. It's normally bad style to have multiple exit points
            // for a function.

            // If they don't have a rigidbody, go on to the next collider.
            if (!targetRigidbody)
                continue;

            Debug.Log("Shell hit: " + targetRigidbody.gameObject.name);

            // Add an explosion force. This is fine for most light to average mass rigidbodies.
            // Don't make it too high though or lighter objects go way too fast and it doesn't look good.
            targetRigidbody.AddExplosionForce(explosionForce, transform.position, explosionRadius);

            ExplodeHeavyObjects(targetRigidbody);

            DoDamage(targetRigidbody);
        }

        HandleParticleEffects();

        // Destroy the shell, since it exploded
        Destroy(transform.parent.gameObject);
    }

    private void DoDamage(Rigidbody targetRigidbody)
    {
        IDamageable damageableObject = targetRigidbody.GetComponentInParent<IDamageable>();

        if (damageableObject != null)
        {
            float damageToDeal = CalculateDamage(targetRigidbody.position);

            string damageID = this.name + Time.time;

            damageableObject.TakeDamage(damageToDeal, damageID, this);
        }
    }

    private float CalculateDamage(Vector3 targetPosition)
    {
        // Create a vector from the shell to the target.
        Vector3 explosionToTarget = targetPosition - transform.position;

        // Calculate the distance from the shell to the target.
        float explosionDistance = explosionToTarget.magnitude;

        // Calculate the proportion of the maximum distance (the explosionRadius) the target is away.
        float relativeDistance = (explosionRadius - explosionDistance) / explosionRadius;

        // Calculate damage as this proportion of the maximum possible damage.
        float damageToDeal;// = relativeDistance * maxDamageToDeal;

        damageToDeal = Mathf.Lerp(minimumDamageToDeal, maxDamageToDeal, relativeDistance);

        // Make sure that the damage is never negative.
        damageToDeal = Mathf.Max(minimumDamageToDeal, damageToDeal);

        return damageToDeal;
    }

    private void ExplodeHeavyObjects(Rigidbody targetRigidbody)
    {
        // Special behavior for heavy things, because otherwise they doesn't move in a very satisfying way when hit.
        IHeavyExplodableObject heavyObject = targetRigidbody.GetComponentInParent<IHeavyExplodableObject>();

        if (heavyObject != null)
        {
            heavyObject.Explode(rigidbody_useThis.velocity.normalized, transform.position, explosionRadius);
        }
    }

    private void HandleParticleEffects()
    {
        // Move the particles off this game object so it doesn't get destroyed when the shell goes away
        float particleDuration = 3;
        particleGameObject.transform.SetParent(null);
        particleGameObject.SetActive(true);

        // the particle duration var doesn't have to be perfect. Just longer than the particle effect.
        // As long as the effect isn't set to loop that is!
        Destroy(particleGameObject, particleDuration);
    }
}
