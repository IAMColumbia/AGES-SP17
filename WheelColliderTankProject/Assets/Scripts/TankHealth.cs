using UnityEngine;
using System.Collections;
using System;

public enum DamageState
{
    NoDamage,
    LightDamage,
    MediumDamage,
    HeavyDamage,
    CriticalDamage
}

public class TankHealth : MonoBehaviour, IDamagable<float>
{
    [SerializeField]
    public ParticleSystem m_ExplosionParticleSystem;

    public float tankHealth = 100;

    protected DamageState _state;
    public DamageState state {
        get { return _state; }
        set { if (_state != value)
                  {
                        _state = DamageState.NoDamage;
                  }
            }
    }
       


    // Use this for initialization
    void Start ()
    {
        this.state = DamageState.NoDamage;
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void TakeDamage(float damageTaken)
    {
        tankHealth = tankHealth - damageTaken;
        if (tankHealth == 75)
        {
            //DamageState value = DamageState.LightDamage;
        }
    }

    public void TankParticleHandler()
    {
        if (this.state == DamageState.NoDamage)
        {
            return;
        }
        else if (this.state == DamageState.LightDamage)
        {
            //TODO: Play particle effects here
        }
    }
}
