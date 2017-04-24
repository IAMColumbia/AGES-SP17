using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : PlaceableObject
{
    public override void SetUpObjectOnGameStart(Tile tile)
    {
        isWalkableObject = false;
        willActivateWhenMovedTo = false;

        currentTile = tile;
    }

    public override PlaceableObjectData GenerateDataClass()
    {
        return new WallData(isWalkableObject, willActivateWhenMovedTo, isActivated);
    }
}





[Serializable]
public class WallData : PlaceableObjectData
{
    public WallData(bool _isWalkableObject, bool _willActivateWhenMovedTo, bool _isActivated) : base(_isWalkableObject, _willActivateWhenMovedTo, _isActivated)
    {
    }
}