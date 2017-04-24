﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndChest : PlaceableObject, IActivatable
{
    public static EndChest endGoal;

    void Awake()
    {
        if (endGoal == null)
        {
            endGoal = this;
        }
        else if (endGoal != this)
        {
            Destroy(this.gameObject);
        }
    }

    public void Activate(GameObject ObjectActivatedBy)
    {
        StateManager.playerState = StateManager.PlayerState.WON_LEVEL;
        Debug.Log("Player has won!  Yaaaaayyyy!");
    }

    public override void SetUpObjectOnGameStart(Tile tile)
    {
        willActivateWhenMovedTo = true;
        isWalkableObject = false;
        isActivated = true;
        currentTile = tile;
    }

    public override PlaceableObjectData GenerateDataClass()
    {
        return new EndChestData(isWalkableObject, willActivateWhenMovedTo, isActivated);
    }
}



[Serializable]
public class EndChestData : PlaceableObjectData
{
    public EndChestData(bool _isWalkableObject, bool _willActivateWhenMovedTo, bool _isActivated) : base(_isWalkableObject, _willActivateWhenMovedTo, _isActivated)
    {
    }
}