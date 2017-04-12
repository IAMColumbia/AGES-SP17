using UnityEngine;
using System.Collections;

public class PickUpObject : MonoBehaviour {

    public Transform player;
    public float throwForce = 10;
    public bool hasPlayer = false;
    bool beingCarried = false;
    public bool isKinematic;
    public Rigidbody rb;

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            hasPlayer = true;
            player = other.gameObject.GetComponent<Transform>();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
            hasPlayer = false;
        
    }

    void Update()
    {
        if (beingCarried)
        {
            //for testing purposes: GetMouseButtonDown(0) is left click, (1) is right click
            if (Input.GetMouseButtonDown(1))
            {
                rb.isKinematic = false;
                transform.parent = null;
                beingCarried = false;
                rb.useGravity = true;
                rb.AddForce(player.GetChild(0).forward * throwForce);
                
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(1) && hasPlayer)
            {
                rb.isKinematic = true;
                rb.useGravity = false;
                transform.parent = player;
                this.transform.SetParent(player.GetChild(0));
                beingCarried = true;
            }
        }
    }
}
