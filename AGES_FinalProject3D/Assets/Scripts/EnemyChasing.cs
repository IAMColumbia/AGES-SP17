using UnityEngine;
using System.Collections;

public class EnemyChasing : MonoBehaviour
{
    [SerializeField]
    private LayerMask layerToCheckForPlayer;
    [SerializeField]
    private float activationRange;

    private GameObject playerToChase;


    // Use this for initialization
    void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        CheckActivationZoneForPlayer();
        ChasePlayer();
	}

    private void CheckActivationZoneForPlayer()
    {
        Collider[] activateZoneArray = Physics.OverlapSphere(gameObject.transform.position, activationRange, layerToCheckForPlayer);

        foreach (Collider player in activateZoneArray)
        {
            playerToChase = player.gameObject;
        }
    }

    private void ChasePlayer()
    {
        if (playerToChase != null)
        {
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, playerToChase.transform.position, Time.deltaTime);
        }
    }
}
