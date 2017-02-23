using UnityEngine;
using System.Collections;

interface IDamageable
{
    void takeDamage(float amount);
}

public class HalfEnemy : MonoBehaviour, IDamageable {

    public Phase Phase
    {
        get; private set;
    }

    public bool Alive { get; private set; }

    public float Health { get; private set; }

    float maxHealth = 100;

    MeshRenderer m_Renderer;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void takeDamage(float amount)
    {

    }
}
