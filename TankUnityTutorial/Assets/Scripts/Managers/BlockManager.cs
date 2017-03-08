using UnityEngine;
//using System.Collections;
using System;

[Serializable]
public class BlockManager
{

    public Transform blockSpawnPoint;

    [HideInInspector]
    public GameObject blockInstance;

    public void EnableSpawner()
    {
        blockInstance.SetActive(true);
    }

    public void DisableSpawner()
    {        
        blockInstance.SetActive(false);
    }

    public void ResetBlocks()
    {
        blockInstance.transform.position = blockSpawnPoint.position;
        blockInstance.transform.rotation = blockSpawnPoint.rotation;
    }
}
