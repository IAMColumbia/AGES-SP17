using UnityEngine;
using UnityEngine.UI;

public class TankShooting : MonoBehaviour
{
    public int PlayerNumber = 1;
    public Rigidbody m_Shell;
    public Transform m_FireTransform;
    public Slider m_AimSlider;
    public AudioSource ShootingAudio;
    public AudioClip m_ChargingClip;
    public AudioClip FireClip, PopClip, SuckingClip;
    public float m_MinLaunchForce = 25f;
    public float m_MaxLaunchForce = 30f;
    public float m_MaxChargeTime = 0.75f;
    public ParticleSystem suckEmmiter;

    private string FireButton, SuckButton;
    private float m_CurrentLaunchForce;
    private float m_ChargeSpeed;
    private bool m_Fired;

    public int MaxNumberOfBullets = 3;
    public float SuckInRadius = 1f;
    public LayerMask bulletLayer;
    [HideInInspector] public int numberOfBullets = 3;                //maybe upgrade this to a list, and you instantiate items from the list... (so I can have a variety of bullets)

    private void OnEnable()
    {
        m_CurrentLaunchForce = m_MinLaunchForce;
        m_AimSlider.value = m_MinLaunchForce;
        suckEmmiter.Stop();
    }


    private void Start()
    {
        FireButton = "Fire1_P" + PlayerNumber;
        SuckButton = "Fire2_P" + PlayerNumber;

        m_ChargeSpeed = (m_MaxLaunchForce - m_MinLaunchForce) / m_MaxChargeTime;
    }

    private void Update()
    {
            DetectFireInput();
            SuckInBullets();
    }

    void DetectFireInput()
    {
        // Track the current state of the fire button and make decisions based on the current launch force.
        m_AimSlider.value = m_MinLaunchForce;

        //if you have bullets to shoot...
        if (numberOfBullets > 0)
        {
            if (m_CurrentLaunchForce >= m_MaxLaunchForce && !m_Fired)
            {
                m_CurrentLaunchForce = m_MaxLaunchForce;
                Fire();
            }
            else if (Input.GetButtonDown(FireButton))
            {
                m_Fired = false;
                m_CurrentLaunchForce = m_MinLaunchForce;

                ShootingAudio.clip = m_ChargingClip;
                ShootingAudio.Play();
            }
            else if (Input.GetButton(FireButton) && !m_Fired)
            {
                m_CurrentLaunchForce += m_ChargeSpeed * Time.deltaTime;
                m_AimSlider.value = m_CurrentLaunchForce;
            }
            else if (Input.GetButtonUp(FireButton) && !m_Fired)
            {
                Fire();
            }
        }
    }

    private void Fire()
    {
        // Instantiate and launch the shell.
        m_Fired = true;

        Rigidbody shellInstance = Instantiate(m_Shell, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody;

        shellInstance.velocity = m_CurrentLaunchForce * m_FireTransform.forward;

        ShootingAudio.clip = FireClip;
        ShootingAudio.Play();

        m_CurrentLaunchForce = m_MinLaunchForce;

        numberOfBullets--;
    }


    private void SuckInBullets()
    {
        if (Input.GetButton(SuckButton))
        {
            if (ShootingAudio.clip != SuckingClip && ShootingAudio.isPlaying == false)
            {
                ShootingAudio.clip = SuckingClip;
                ShootingAudio.loop = true;
                ShootingAudio.volume = 0.25f;
                ShootingAudio.Play();

                suckEmmiter.Play();
            }

            Collider[] colliders = Physics.OverlapSphere(m_FireTransform.position, SuckInRadius, bulletLayer);
            gameObject.GetComponent<TankHealth>().TakeDamage(0.25f);

            //loop through all the bullets
            for (int x = colliders.Length - 1; x >= 0; x--)
            {
                //destroy the bullets and add it to the number of bullets you can shoot
                Collider targetCollider = colliders[x];

                if (targetCollider.gameObject.tag == "Pickup")
                {
                    if (targetCollider.gameObject.GetComponent<Pickup>().OnCollected(this))
                    {
                        //targetCollider.gameObject.SetActive(false);

                        //sound effect from: https://www.youtube.com/watch?v=wJaOs-s-cGU
                        ShootingAudio.clip = PopClip;
                        ShootingAudio.loop = false;
                        ShootingAudio.volume = 1f;
                        ShootingAudio.Play();
                    }
                    
                }
                else if (numberOfBullets < MaxNumberOfBullets)
                {
                    numberOfBullets++;
                    Destroy(colliders[x].gameObject);

                    //sound effect from: https://www.youtube.com/watch?v=wJaOs-s-cGU
                    ShootingAudio.clip = PopClip;
                    ShootingAudio.loop = false;
                    ShootingAudio.volume = 1f;
                    ShootingAudio.Play();
                }


            }
        }
        else if(Input.GetButtonUp(SuckButton))
        {
            ShootingAudio.loop = false;
            ShootingAudio.Stop();
            ShootingAudio.volume = 1f;
            ShootingAudio.clip = null;

            suckEmmiter.Stop();
        }
    }

    public void ReloadTank()
    {
        numberOfBullets = MaxNumberOfBullets;
    }
}