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

    public void takeDamage(Bullet bullet)
    {
        Health -= bullet.damage;
        if (Health <= 0 && Alive)
        {
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
