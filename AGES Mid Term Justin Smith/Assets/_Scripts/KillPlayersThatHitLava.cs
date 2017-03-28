using UnityEngine;
using System.Collections;

public class KillPlayersThatHitLava : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage();
    }
}
