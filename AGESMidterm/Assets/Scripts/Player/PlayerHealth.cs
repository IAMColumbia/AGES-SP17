using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour {

    public int StartingHealth = 100;
    public Slider HealthSlider;
    public Image HealthFillImage;
    public Color FullHealthColor;
    public GameObject OnDeathParticleSystemPrefab;

    private PlayerManager playerManager;
    private AudioSource PlayerDeathSound;
    private ParticleSystem OnDeathParticleSystem;
    private float currentHealth;
    private bool isDead;

    private void Awake()
    {
        OnDeathParticleSystem = Instantiate(OnDeathParticleSystemPrefab).GetComponent<ParticleSystem>();
        PlayerDeathSound = OnDeathParticleSystem.GetComponent<AudioSource>();

        OnDeathParticleSystem.gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        currentHealth = StartingHealth;
        isDead = false;

        SetHealthUI();
    }

    public void TakeDamage()
    {
        int damageTaken = 20;
        currentHealth -= damageTaken;

        SetHealthUI();
        if(currentHealth <= 0 & !isDead)
        {
            OnDeath();
        }
        else
            StartCoroutine(PlayerFlash());


    }

    private void SetHealthUI()
    {
        HealthSlider.value = currentHealth;

        HealthFillImage.color = FullHealthColor;
    }

    private void OnDeath()
    {
        isDead = true;

        OnDeathParticleSystem.transform.position = transform.position;
        OnDeathParticleSystem.gameObject.SetActive(true);

        OnDeathParticleSystem.Play();
        PlayerDeathSound.Play();

        gameObject.SetActive(false);
    }
    
    private IEnumerator PlayerFlash()
    {
        for (int i = 0; i < 10; i++)
        {
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            yield return new WaitForSeconds(.1f);
            gameObject.GetComponent<MeshRenderer>().enabled = true;
            yield return new WaitForSeconds(.1f);
        }
    }
}
