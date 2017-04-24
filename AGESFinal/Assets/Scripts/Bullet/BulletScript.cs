using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

    [SerializeField]
    private LayerMask ButtMask;

    [SerializeField]
    public ParticleSystem ExplosionParticles;

    [SerializeField]
    public AudioSource ExplosionAudio;

    [SerializeField]
    public float MaxLifeTime = 2f;

    [SerializeField]
    public float SearchRadius = 5f;

    public Transform shooter;

    void Start()
    {
        Destroy(gameObject, MaxLifeTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Collider2D[] buttcolliders = Physics2D.OverlapCircleAll(transform.position, SearchRadius, ButtMask);
        
        for (int i = 0; i < buttcolliders.Length; i++)
        {
            BoxCollider2D targetButt = buttcolliders[i].GetComponent<BoxCollider2D>();

            if (!targetButt)
                continue;

            Debug.Log("Shell hit: " + targetButt.gameObject.name);

            PlayerHealth targetHealth = targetButt.GetComponentInParent<PlayerHealth>();

            if (!targetHealth)
                continue;

            targetHealth.CueDeath();

            shooter.SendMessage("AddToScore", 1);
            
        }


        ExplosionParticles.transform.parent = null;
        ExplosionParticles.Play();

        ExplosionAudio.Play();

        Destroy(ExplosionParticles.gameObject, ExplosionParticles.duration);
        Destroy(gameObject);

    }
}
