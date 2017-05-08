using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIPlaceableItem : MonoBehaviour
{
    [SerializeField] GameObject itemPrefab; 
    Controller controller;

    private void Start()
    {
        controller = GameObject.FindGameObjectWithTag("PlayerController").GetComponent<Controller>();
    }

    public void SelectItemToPlace()
    {
        //INFO GOES FROM HERE -> CONTROLLER -> TILE
        controller.selectedObjectInEditor = itemPrefab;
    }
}
