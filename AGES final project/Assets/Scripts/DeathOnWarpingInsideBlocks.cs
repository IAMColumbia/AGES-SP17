using UnityEngine;
using System.Collections;

public class DeathOnWarpingInsideBlocks : MonoBehaviour
{
    Player player;

    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            player.isAlive = false;
        }
    }
}
