using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class PlayerManager {

    public Transform SpawnPoint;
    [HideInInspector]
    public float PlayerNumber;
    [HideInInspector]
    public GameObject Instance;
    //[HideInInspector]
    public int ButtsBlasted;
    
    private PlayerController movement;

    public void Setup()
    {
        movement = Instance.GetComponent<PlayerController>();

        movement.playerNumber = PlayerNumber;
 
    }

    public void DisableControl()
    {
        movement.enabled = false;
    }

    public void EnableControl()
    {
        movement.enabled = false;
    }

    public void Reset()
    {
        if(Instance != null)
        {
            Instance.transform.position = SpawnPoint.position;
            Instance.transform.rotation = SpawnPoint.rotation;

            Instance.SetActive(false);
            Instance.SetActive(true);
        }
    }
}
