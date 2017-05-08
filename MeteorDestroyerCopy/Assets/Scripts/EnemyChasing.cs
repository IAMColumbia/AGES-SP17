using UnityEngine;
using System.Collections;

public class EnemyChasing : MonoBehaviour
{
    [SerializeField]
    private LayerMask layerToCheckForPlayer;
    [SerializeField]
    private float activationRange;

    private Vector3 playerToChasePosition;
    private Vector3 nullChasePosition = new Vector3();

    private Transform physicalBody;

    // Use this for initialization
    void Start ()
    {
        playerToChasePosition = nullChasePosition;
        physicalBody = gameObject.transform.parent;
	}
	
	// Update is called once per frame
	void Update ()
    {
        ChasePlayer();
	}

    private void ChasePlayer()
    {
        if (playerToChasePosition != nullChasePosition)
        {
            //gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, playerToChasePosition, Time.deltaTime);
            gameObject.transform.position = Vector3.Lerp(physicalBody.gameObject.transform.position, playerToChasePosition, Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerToChasePosition = other.transform.position;
        }
    }
}
