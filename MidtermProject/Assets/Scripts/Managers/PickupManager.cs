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
    float speedIncreaseMultiplier = 2;
    [SerializeField]
    float shieldDurationInSeconds = 10;

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
                DoubleDamage(other.gameObject);
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
        //TODO: play sound effect

        tank.GetComponent<TankMovement>().m_Speed *= speedIncreaseMultiplier;

        Destroy(this.gameObject);
    }

    void DoubleDamage(GameObject tank)
    {
        //TODO: play sound effect

        tank.GetComponent<TankShooting>().DoubleShotIsActive = true;

        Destroy(this.gameObject);
    }

    void Shield(GameObject tank)
    {
        //TODO: play sound effect

        StartCoroutine(GenerateShield(tank));

        this.GetComponent<SphereCollider>().enabled = false;
        this.GetComponent<MeshRenderer>().enabled = false;
        this.GetComponent<ParticleSystem>().Stop();
        this.GetComponent<Light>().enabled = false;
    }

    IEnumerator GenerateShield(GameObject tank)
    {
        tank.GetComponent<TankHealth>().ShieldIsActive = true;

        yield return new WaitForSeconds(shieldDurationInSeconds);

        tank.GetComponent<TankHealth>().ShieldIsActive = false;

        Destroy(this.gameObject);
    }
}
