using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TankHealth : MonoBehaviour
{
    [SerializeField]
    public Slider slider;

    public float StartingHealth = 100f;                                          
    public Image FillImage;                           
    public Color FullHealthColor = Color.green;       
    public Color ZeroHealthColor = Color.red;         
    public GameObject ExplosionPrefab;                


    private ParticleSystem ExplosionParticles;        
    private float CurrentHealth;                     
    private bool Dead;                               


    private void Awake()
    {
        // Instantiate the explosion prefab and get a reference to the particle system on it.
        ExplosionParticles = Instantiate(ExplosionPrefab).GetComponent<ParticleSystem>();

        // Disable the prefab so it can be activated when it's required.
        ExplosionParticles.gameObject.SetActive(false);
    }


    private void OnEnable()
    {
        // When the tank is enabled, reset the tank's health and whether or not it's dead.
        CurrentHealth = StartingHealth;
        Dead = false;

        // Update the health slider's value and color.
        SetHealthUI();
    }


    public void TakeDamage(float amount)
    {
        // Reduce current health by the amount of damage done.
        CurrentHealth -= amount;

        // Change the UI elements appropriately.
        SetHealthUI();

        // If the current health is at or below zero and it has not yet been registered, call OnDeath.
        if (CurrentHealth <= 0f && !Dead)
        {
            OnDeath();
        }
    }


    private void SetHealthUI()
    {
        // Set the slider's value appropriately.
        slider.value = CurrentHealth;

        // Interpolate the color of the bar between the choosen colours based on the current percentage of the starting health.
        FillImage.color = Color.Lerp(ZeroHealthColor, FullHealthColor, CurrentHealth / StartingHealth);
    }


    private void OnDeath()
    {
        // Set the flag so that this function is only called once.
        Dead = true;

        // Move the instantiated explosion prefab to the tank's position and turn it on.
        ExplosionParticles.transform.position = transform.position;
        ExplosionParticles.gameObject.SetActive(true);

        // Play the particle system of the tank exploding.
        ExplosionParticles.Play();

        // Turn the tank off.
        gameObject.SetActive(false);
    }
}
