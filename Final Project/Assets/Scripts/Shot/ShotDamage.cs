using UnityEngine;

public class ShotDamage : MonoBehaviour
{
    public LayerMask m_PlayerMask;
    public ParticleSystem m_ShotParticles;       
    //public AudioSource m_DamageAudio;                              
    public float m_ExplosionForce = 1000f;            
    public float m_MaxLifeTime = 2f;                  
    public float m_ExplosionRadius = 5f;              


    private void Start()
    {
        Destroy(gameObject, m_MaxLifeTime);
    }


    private void OnTriggerEnter(Collider other)
    {
		Collider[] colliders = Physics.OverlapSphere(transform.position, m_ExplosionRadius, m_PlayerMask);

		for (int i = 0; i < colliders.Length; i++) 
		{
			Rigidbody targetRigidbody = colliders [i].GetComponent<Rigidbody> ();

			if (!targetRigidbody)
				continue;

			targetRigidbody.AddExplosionForce (m_ExplosionForce, transform.position, m_ExplosionRadius);
		}

		//m_ShotParticles.transform.parent = null;

		//m_ShotParticles.Play ();

		//m_DamageAudio.Play ();

		//Destroy (m_ShotParticles.gameObject, m_ShotParticles.duration);

		Destroy (gameObject);
    }

}