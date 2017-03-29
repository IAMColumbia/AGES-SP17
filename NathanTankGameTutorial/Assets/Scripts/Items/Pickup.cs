using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pickup : MonoBehaviour
{
    public float rotationSpeed = 25f;
    public bool canRespawnOnTimer = false;
    public float respawnDelay = 3f;

    static List<GameObject> listOfAllPickups = new List<GameObject>();

    protected void Start()
    {
        listOfAllPickups.Add(this.gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        //rotate
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }

    protected IEnumerator DeactivateSelf()
    {
        //yield return null;

        if(!canRespawnOnTimer)
        {
            gameObject.SetActive(false);
        }
        else
        {
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<BoxCollider>().enabled = false;
            yield return new WaitForSeconds(respawnDelay);
            GetComponent<MeshRenderer>().enabled = true;
            GetComponent<BoxCollider>().enabled = true;
        }
    }

    public static void RespawnAllPickups()
    {
        foreach(GameObject g in listOfAllPickups)
        {
            if (g) //if the gameObject exists...
            {
                g.SetActive(true);

                if (g.GetComponent<MeshRenderer>()) { g.GetComponent<MeshRenderer>().enabled = true; }
                if (g.GetComponent<BoxCollider>()) { g.GetComponent<BoxCollider>().enabled = true; }
            }
        }
    }

    public virtual bool OnCollected(TankShooting targetTank) { return false; }
}

