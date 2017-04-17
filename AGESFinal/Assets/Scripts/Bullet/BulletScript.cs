using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

    [SerializeField]
    private LayerMask PlayerMask;

    [SerializeField]
    public ParticleSystem ExplosionParticles;

    [SerializeField]
    public AudioSource ExplosionAudio;

    [SerializeField]
    public float MaxLifeTime = 2f;

    [SerializeField]
    public float SearchRadius = 5f;
    

    void Start()
    {
        Destroy(gameObject, MaxLifeTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Collider2D[] playercolliders = Physics2D.OverlapCircleAll(transform.position, SearchRadius, PlayerMask);

        for (int i = 0; i < playercolliders.Length; i++)
        {
            Rigidbody2D targetRigidbody = playercolliders[i].GetComponent<Rigidbody2D>();

            if (!targetRigidbody)
                continue;

            Debug.Log("Shell hit: " + targetRigidbody.gameObject.name);

            //PlayerHealth targetHealth = targetRigidbody.GetComponent<PlayerHealth>();

            //if (!targetHealth)
            //    continue;

            //targetHealth.TakeDamage();

        }

        ExplosionParticles.transform.parent = null;
        ExplosionParticles.Play();

        ExplosionAudio.Play();

        Destroy(ExplosionParticles.gameObject, ExplosionParticles.duration);
        Destroy(gameObject);
    }
}
