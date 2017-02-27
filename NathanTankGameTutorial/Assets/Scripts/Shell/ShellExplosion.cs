using UnityEngine;

public class ShellExplosion : MonoBehaviour
{
    public LayerMask TankMask;
    public ParticleSystem ExplosionParticles;       
    public AudioSource ExplosionAudio;              
    public float MaxDamage = 100f;                  
    public float MaxLifeTime = 100f;                  
    public float ExplosionRadius = 5f;
    public float BounceVelocity = 10f;

    private float launchSpeed;

    public static bool DestroyAllBullets;


    private void Start()
    {
        Destroy(gameObject, MaxLifeTime);
    }

    void Update()
    {
        if (DestroyAllBullets)
            Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag != "Players")
        {
            // force is how forcefully we will push the player away from the enemy.

            // Calculate Angle Between the collision point and the player
            Vector3 dir = other.contacts[0].point - transform.position;
            // We then get the opposite (-Vector3) and normalize it
            dir = -dir.normalized;
            // And finally we add force in the direction of dir and multiply it by force. 
            // This will push back the player
            GetComponent<Rigidbody>().velocity = dir * BounceVelocity;
        }

        LayerMask otherLayer = other.gameObject.layer;

        if (otherLayer == LayerMask.NameToLayer("Players"))
        {
            Debug.Log("Tank was hit");

            Rigidbody targetRigidbody = other.gameObject.GetComponent<Rigidbody>();

            TankHealth targetHealth = targetRigidbody.GetComponentInChildren<TankHealth>();
            Debug.Log(targetHealth);
            float damage = CalculateDamage(targetRigidbody.position);
            targetHealth.TakeDamage(damage);

            DestroySelf();
        }

    }

    void DestroySelf()
    {
        ExplosionParticles.transform.parent = null;
        ExplosionParticles.Play();
        ExplosionAudio.Play();

        Destroy(ExplosionParticles.gameObject, ExplosionParticles.duration);
        Destroy(gameObject);
    }

    private float CalculateDamage(Vector3 targetPosition)
    {
        // Calculate the amount of damage a target should take based on it's position.
        Vector3 explosionToTarget = targetPosition - transform.position;

        float explosionDistance = explosionToTarget.magnitude;
        float relativeDistance = (ExplosionRadius - explosionDistance) / ExplosionRadius;

        float damage = relativeDistance * MaxDamage;
        damage = Mathf.Max(0, damage);


        Debug.Log(damage);
        return damage;
    }
}