using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : PlaceableObject
{
    public override void SetUpObjectOnGameStart(Tile tile)
    {
        //set up the spike trap
        isWalkableObject = true;
        willActivateWhenMovedTo = true;
        isActivated = true;

        currentTile = tile;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<BasicCharacter>())
        {
            other.GetComponent<BasicCharacter>().Kill();
        }
    }

    public override PlaceableObjectData GenerateDataClass()
    {
        return new SpikeTrapData(isWalkableObject, willActivateWhenMovedTo, isActivated);
    }
}





[Serializable]
public class SpikeTrapData : PlaceableObjectData
{
    public SpikeTrapData(bool _isWalkableObject, bool _willActivateWhenMovedTo, bool _isActivated) : base(_isWalkableObject, _willActivateWhenMovedTo, _isActivated)
    {
    }
}