using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField]
    float rotationSpeed = 15.0f;
    [SerializeField]
    float maxLightRange = 25.0f;
    [SerializeField]
    float lightPulseSpeed = 5.0f;
    [SerializeField]
    float speedIncreaseMultiplier = 2.0f;
    [SerializeField]
    float shieldDurationInSeconds = 5.0f;

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
        if (this.CompareTag("SpeedBoostPickup"))
        {
            SpeedBoost(other.gameObject);
        }
        else if (this.CompareTag("DoubleDamagePickup"))
        {
            DoubleDamage(other.gameObject);
        }
        else if (this.CompareTag("DoubleDamagePickup"))
        {
            Shield(other.gameObject);
        }
        else
        {
            Debug.Log("Not a valid pickup tag!!!");
        }
    }

    void SpeedBoost(GameObject tank)
    {
        tank.GetComponent<TankMovement>().m_Speed *= speedIncreaseMultiplier;

        Destroy(this.gameObject);
    }

    void DoubleDamage(GameObject tank)
    {
        tank.GetComponent<TankShooting>().DoubleShotIsActive = true;

        Destroy(this.gameObject);
    }

    void Shield(GameObject tank)
    {
        //TODO: implement shield

        Destroy(this.gameObject);
    }
}
