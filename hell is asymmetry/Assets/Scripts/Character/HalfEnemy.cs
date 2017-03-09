using UnityEngine;
using System.Collections;

public class HalfEnemy : MonoBehaviour, IDamageable {

    Enemy parent;


    public bool Alive { get; private set; }

    public float Health { get; private set; }

    [SerializeField]
    MeshRenderer m_Renderer;
    [SerializeField]
    Collider2D m_Collider;
    [SerializeField]
    float score = 100;

	// Use this for initialization
	void Start () {
        parent = GetComponentInParent<Enemy>();
        Health = parent.MaxHealth;
        m_Renderer.material = parent.positiveMaterial;
        Alive = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator flashOnDamageTaken(float time)
    {
        m_Renderer.material = parent.damageMaterial;

        yield return new WaitForSeconds(time);

        m_Renderer.material = parent.positiveMaterial;
    }

    public void takeDamage(Bullet bullet)
    {
        Health -= bullet.damage;

        StartCoroutine(flashOnDamageTaken(time: 0.1f));

        if (Health <= 0 && Alive)
        {
            StopAllCoroutines();
            bullet.owner.AddScore(score);
            Die();
        }
        Destroy(bullet.gameObject);
    }

    void Die()
    {
        Alive = false;
        m_Renderer.material = parent.negativeMaterial;
        m_Collider.enabled = false;
    }
}
