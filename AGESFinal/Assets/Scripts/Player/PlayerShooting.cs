using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour
{
    GameManager gmanager;
    private void Start()
    {
        gmanager = FindObjectOfType<GameManager>();
    }
    private void Update()
    {
        if(Input.GetKey(KeyCode.Space))
            gmanager.StoreButtsBlasted();
    }

}
