using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    //public float m_StartingHealth = 100f;
    //public Slider m_Slider;
    //public Image m_FillImage;
    //public Color m_FullHealthColor = Color.green;
    //public Color m_ZeroHealthColor = Color.red;
    //public GameObject m_ExplosionPrefab;
    //public GameObject Smoke;

    //private AudioSource m_ExplosionAudio;
    //private ParticleSystem m_ExplosionParticles;
    //private ParticleSystem SmokeParticle;

    //public float m_CurrentHealth;
    private bool m_Dead;


    private void Awake()
    {
        //m_ExplosionParticles = Instantiate(m_ExplosionPrefab).GetComponent<ParticleSystem>();
        //m_ExplosionAudio = m_ExplosionParticles.GetComponent<AudioSource>();
        //m_ExplosionParticles.gameObject.SetActive(false);

        //SmokeParticle = Instantiate(Smoke).GetComponent<ParticleSystem>();
        //Smoke.gameObject.SetActive(false);
    }


    private void OnEnable()
    {
        //m_CurrentHealth = m_StartingHealth;
        m_Dead = false;

        //SetHealthUI();
    }


    public void TakeDamage()
    {
        // Adjust the tank's current health, update the UI based on the new health and check whether or not the tank is dead.
        //m_CurrentHealth -= amount;
        //SetHealthUI();
        Debug.Log("Take Damage");
        //if (m_CurrentHealth < 1f && !m_Dead)
        //{
        //    SmokeParticle.transform.position = transform.position;
        //    Smoke.gameObject.SetActive(true);
        //}
        //if (m_CurrentHealth <= .75f && !m_Dead)
        //{

        //    SmokeParticle.startSize = 2;

        //}
        //if (m_CurrentHealth <= .50f && !m_Dead)
        //{
        //    SmokeParticle.startSize = 5;
        //}
        //else if (m_CurrentHealth <= 0f && !m_Dead)
        //{
        //    OnDeath();
        //    SmokeParticle.startSize = 6;
        //}
        OnDeath();
    }


    //private void SetHealthUI()
    //{
    //    // Adjust the value and colour of the slider.
    //    m_Slider.value = m_CurrentHealth;
    //    m_FillImage.color = Color.Lerp(m_ZeroHealthColor, m_FullHealthColor, m_CurrentHealth / m_StartingHealth);
    //}


    private void OnDeath()
    {
        // Play the effects for the death of the tank and deactivate it.
        m_Dead = true;
        //m_ExplosionParticles.transform.position = transform.position;
        //m_ExplosionParticles.gameObject.SetActive(true);

        //m_ExplosionParticles.Play();
        //m_ExplosionAudio.Play();
        gameObject.SetActive(false);
    }

    //public void Damage(float damageTaken)
    //{
    //    //damageTaken = 1;
    //    //if (damageTaken == 1)
    //    //{

    //    //}
    //}

    public void Kill()
    {
        throw new NotImplementedException();
    }
}
