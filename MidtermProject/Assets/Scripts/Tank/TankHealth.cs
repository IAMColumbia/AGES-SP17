using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TankHealth : MonoBehaviour
{
    public float m_StartingHealth = 100f;          
    public Slider m_Slider;                        
    public Image m_FillImage;                      
    public Color m_FullHealthColor = Color.green;  
    public Color m_ZeroHealthColor = Color.red;    
    public GameObject m_ExplosionPrefab;
    
    AudioSource m_ExplosionAudio;          
    ParticleSystem m_ExplosionParticles;   
    float m_CurrentHealth;  
    bool m_Dead;

    bool shieldIsActive = false;

    public bool ShieldIsActive
    {
        set
        {
            shieldIsActive = value;
        }
    }

    void Awake()
    {
        m_ExplosionParticles = Instantiate(m_ExplosionPrefab).GetComponent<ParticleSystem>();
        m_ExplosionAudio = m_ExplosionParticles.GetComponent<AudioSource>();

        m_ExplosionParticles.gameObject.SetActive(false);
    }

    void OnEnable()
    {
        m_CurrentHealth = m_StartingHealth;
        m_Dead = false;

        SetHealthUI();
    }

    public void TakeDamage(float amount)
    {
        if (!shieldIsActive)
        {
            // Adjust the tank's current health, update the UI based on the new health and check whether or not the tank is dead.

            m_CurrentHealth -= amount;
            SetHealthUI();

            if (m_CurrentHealth <= 0f && !m_Dead)
            {
                OnDeath();
            }
        }
    }

    void SetHealthUI()
    {
        // Adjust the value and colour of the slider.

        m_Slider.value = m_CurrentHealth;
        m_FillImage.color = Color.Lerp(m_ZeroHealthColor, m_FullHealthColor, m_CurrentHealth / m_StartingHealth);
    }

    void OnDeath()
    {
        // Play the effects for the death of the tank and deactivate it.

        m_Dead = true;

        m_ExplosionParticles.transform.position = transform.position;
        m_ExplosionParticles.gameObject.SetActive(true);

        m_ExplosionParticles.Play();
        m_ExplosionAudio.Play();

        gameObject.SetActive(false);
    }
}