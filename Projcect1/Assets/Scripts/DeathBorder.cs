using UnityEngine;
using System.Collections;

public class DeathBorder : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            TankDamage playerDamage = other.GetComponent<TankDamage>();
            playerDamage.Explode();
            other.gameObject.SetActive(false);
        }
    }
}
