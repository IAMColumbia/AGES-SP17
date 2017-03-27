using UnityEngine;
using System.Collections;

public class BulletExplode : MonoBehaviour {

    public LayerMask PlayerMask;
    public ParticleSystem ExplosionParticles;
    public AudioSource ExplosionAudio;
    public float MaxLifeTime = 2f;
    public float SearchRadius = 5f;


	void Start () {
        Destroy(gameObject, MaxLifeTime);
	}

    private void OnTriggerEnter(Collider other)
    {
        Collider[] playercolliders = Physics.OverlapSphere(transform.position, SearchRadius, PlayerMask);

        for (int i = 0; i < playercolliders.Length; i++)
        {
            Rigidbody targetRigidbody = playercolliders[i].GetComponent<Rigidbody>();

            if (!targetRigidbody)
                continue;

            Debug.Log("Shell hit: " + targetRigidbody.gameObject.name);

            PlayerHealth targetHealth = targetRigidbody.GetComponent<PlayerHealth>();

            if (!targetHealth)
                continue;

            targetHealth.TakeDamage();
            
        }

        ExplosionParticles.transform.parent = null;
        ExplosionParticles.Play();

        ExplosionAudio.Play();

        Destroy(ExplosionParticles.gameObject, ExplosionParticles.duration);
        Destroy(gameObject);
    }
    
}
