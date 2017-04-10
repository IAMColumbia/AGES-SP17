using UnityEngine;
using System.Collections;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            Destroy(other.gameObject);
    }

}
