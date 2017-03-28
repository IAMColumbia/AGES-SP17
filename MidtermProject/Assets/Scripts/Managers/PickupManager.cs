using UnityEngine;
using System.Collections;

public class PickupManager : MonoBehaviour
{
    [SerializeField]
    float rotationSpeed;
    [SerializeField]
    float maxLightRange;
    [SerializeField]
    float lightPulseSpeed;

    [SerializeField]
    float speedIncreaseMultiplier = 2.0f;
    [SerializeField]
    float speedInceaseDurationInSeconds = 10.0f;
    [SerializeField]
    float doubleShotDurationInSeconds = 10.0f;
    [SerializeField]
    float shieldDurationInSeconds = 10.0f;

    AudioSource powerUpSFX;
    AudioSource powerDownSFX;

    void Start()
    {
        AudioSource[] sources = this.GetComponents<AudioSource>();

        powerUpSFX = sources[0];
        powerDownSFX = sources[1];
    }

    void Update()
    {
        PulsateLight();
	}

    void PulsateLight()
    {
        this.transform.Rotate(Vector3.up * (Time.deltaTime * rotationSpeed));
        this.GetComponent<Light>().range = Mathf.PingPong(Time.time * lightPulseSpeed, maxLightRange);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("TankShell"))
        {
            if (this.CompareTag("SpeedBoostPickup"))
            {
                SpeedBoost(other.gameObject);
            }
            else if (this.CompareTag("DoubleDamagePickup"))
            {
                DoubleShot(other.gameObject);
            }
            else if (this.CompareTag("ShieldPickup"))
            {
                Shield(other.gameObject);
            }
            else
            {
                Debug.Log("Not a valid pickup tag!!!");
            }
        }
    }

    void SpeedBoost(GameObject tank)
    {
        powerUpSFX.Play();

        StartCoroutine(ActivateSpeedBoost(tank));

        this.GetComponent<SphereCollider>().enabled = false;
        this.GetComponent<MeshRenderer>().enabled = false;
        this.GetComponent<ParticleSystem>().Stop();
        this.GetComponent<Light>().enabled = false;
    }

    void DoubleShot(GameObject tank)
    {
        powerUpSFX.Play();

        StartCoroutine(ActivateDoubleShot(tank));

        this.GetComponent<SphereCollider>().enabled = false;
        this.GetComponent<MeshRenderer>().enabled = false;
        this.GetComponent<ParticleSystem>().Stop();
        this.GetComponent<Light>().enabled = false;
    }

    void Shield(GameObject tank)
    {
        powerUpSFX.Play();

        StartCoroutine(GenerateShield(tank));

        this.GetComponent<SphereCollider>().enabled = false;
        this.GetComponent<MeshRenderer>().enabled = false;
        this.GetComponent<ParticleSystem>().Stop();
        this.GetComponent<Light>().enabled = false;
    }

    IEnumerator ActivateSpeedBoost(GameObject tank)
    {
        tank.GetComponent<TankMovement>().TankMovementSpeedMultiplier = speedIncreaseMultiplier;
        yield return new WaitForSeconds(speedInceaseDurationInSeconds);
        tank.GetComponent<TankMovement>().TankMovementSpeedMultiplier = 1.0f;

        powerDownSFX.Play();

        Destroy(this.gameObject);
    }

    IEnumerator ActivateDoubleShot(GameObject tank)
    {
        tank.GetComponent<TankShooting>().DoubleShotIsActive = true;
        yield return new WaitForSeconds(doubleShotDurationInSeconds);
        tank.GetComponent<TankShooting>().DoubleShotIsActive = false;

        powerDownSFX.Play();

        Destroy(this.gameObject);
    }

    IEnumerator GenerateShield(GameObject tank)
    {
        tank.GetComponent<TankHealth>().ShieldIsActive = true;
        yield return new WaitForSeconds(shieldDurationInSeconds);
        tank.GetComponent<TankHealth>().ShieldIsActive = false;

        powerDownSFX.Play();

        Destroy(this.gameObject);
    }
}
