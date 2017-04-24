using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIPlaceableItem : MonoBehaviour
{
    [SerializeField] GameObject itemPrefab; //TODO: possibly, this can be "PlaceableObject" instead of "GameObject".
                                            //If so, Tile's "SetItemToTile" method can be simplified AND it would be garaunteed that ONLY placeable objects can be placed
                                            //aka: it would make ALL the code a lot safer.  And less info would be passed around as a bonus
    Controller controller;

    private void Start()
    {
        //TODO: TRY TO FIND A DIFFERENT WAY THAN FindGameObjectWithTag
        controller = GameObject.FindGameObjectWithTag("PlayerController").GetComponent<Controller>();
    }

    public void SelectItemToPlace()
    {
        //Debug.Log("Level editor item selected: " + itemPrefab.name);

        //INFO GOES FROM HERE -> CONTROLLER -> TILE
        controller.selectedObjectInEditor = itemPrefab;
    }
}
