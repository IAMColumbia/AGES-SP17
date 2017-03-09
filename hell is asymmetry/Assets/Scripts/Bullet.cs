using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    Vector2 velocity = new Vector2(0, 0);
    public Character owner;

    public float damage = 1;

    [SerializeField]
    Renderer m_renderer;

    [SerializeField]
    Collider2D m_collider;

    [SerializeField]
    Material positiveMaterial, negativeMaterial;

    float lifespan = 10;

	// Use this for initialization
	void Start () {
        Destroy(this.gameObject, lifespan);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Init(bool active, int layer, Vector2 _velocity, Character _owner)

    {
        m_renderer.material = active ? positiveMaterial : negativeMaterial;
        m_collider.enabled = active;
        setLayerRecursive(layer, gameObject);
        velocity = _velocity;
        owner = _owner;
    }

    private void setLayerRecursive(int layer, GameObject currentObject)
    {
        currentObject.layer = layer;
        int childCount = currentObject.transform.childCount;
        for(int i = 0; i < childCount; i++)
        {
            GameObject nextObject = currentObject.transform.GetChild(i).gameObject;
            setLayerRecursive(layer, nextObject);
        }
    }

    private void FixedUpdate()
    {
        UpdatePosition(Time.fixedDeltaTime);
    }

    void UpdatePosition(float deltaTime)
    {
        Vector3 newPosition = transform.position;
        newPosition.x += velocity.x * deltaTime;
        newPosition.y += velocity.y * deltaTime;
        transform.position = newPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //bullets should not interact with other bullets, the boundary encompassing the screen, or gameobjects on the same "team"

        string ownerTag = owner != null ? owner.gameObject.tag : "";

        if (!(collision.gameObject.tag == "Bullet" || collision.gameObject.tag == "Boundary" || collision.gameObject.tag == ownerTag))
        {
            IDamageable damageableObject = collision.GetComponent<IDamageable>();

            if (damageableObject != null)
            {
                damageableObject.takeDamage(this);
            }
            m_renderer.material = negativeMaterial;
            m_collider.enabled = false;
        }
    }
}
