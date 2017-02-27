using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pickup : MonoBehaviour
{
    public float rotationSpeed = 25f;

    static List<GameObject> listOfAllPickups = new List<GameObject>();

    void Start()
    {
        listOfAllPickups.Add(this.gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        //rotate
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }

    public static void RespawnAllPickups()
    {
        foreach(GameObject g in listOfAllPickups)
        {
            g.SetActive(true);
        }
    }

    public virtual bool OnCollected(TankShooting targetTank) { return false; }
}

