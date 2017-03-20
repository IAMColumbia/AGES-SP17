using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{

    [SerializeField]
    float playerHealth;

    // Use this for initialization
    void Start()
    {
        playerHealth = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth < 0)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name != gameObject.name)
        {
            playerHealth = playerHealth - 0.15f;
        }
    }
}
