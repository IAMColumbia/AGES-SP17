using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlaceableObject : MonoBehaviour
{
    [HideInInspector] public bool isWalkableObject = true;          //can be walked on
    [HideInInspector] public bool willActivateWhenMovedTo = false;  //will be activated when a character tries to move to this location
    [HideInInspector] public bool isActivated = true;               //is this object currently activated?

    public static PlaceableObject currentObjectInteractedWith = null;
    public Tile currentTile;

    //This should be called whenever the level begins
    /// <summary>
    /// Set up isWalkableObject and willActivateWhenMovedTo in here!
    /// </summary>
    /// <param name="tile"></param>
    public abstract void SetUpObjectOnGameStart(Tile tile);

    public virtual void DeselectInteractableObject() { }

    public virtual void LateSetup(Tile neededTile) { }

    public abstract PlaceableObjectData GenerateDataClass();
}



[Serializable]
public class PlaceableObjectData
{
    bool isWalkableObject;
    bool willActivateWhenMovedTo;
    bool isActivated;

    public PlaceableObjectData(bool _isWalkableObject, bool _willActivateWhenMovedTo, bool _isActivated)
    {
        isWalkableObject = _isWalkableObject;
        willActivateWhenMovedTo = _willActivateWhenMovedTo;
        isActivated = _isActivated;
    }


}