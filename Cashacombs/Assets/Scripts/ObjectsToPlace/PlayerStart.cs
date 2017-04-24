using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStart : PlaceableObject
{

    //TODO: THIS CLASS CAN BE ABSTRACTED TO SPAWN ANY CREATURE!!!!
    //lift SetupObject from this class, and change spawnedPlayer.GetComponent<Player>() to spawnedPlayer.GetComponent<BasicCharacter>();
    [SerializeField] Player playerPrefab;

    //NOTE: I should prevent players from placing multiple playerStarts in the game
    //However, this is a safety check to MAKE SURE there's no other playerStarts that exist
    public static PlayerStart playerStart;
    void Awake()
    {
        if(playerStart == null)
        {
            playerStart = this;
        }
        else if (playerStart != this)
        {
            Destroy(this.gameObject);
        }
    }

    public override void SetUpObjectOnGameStart(Tile tile)
    {
        isWalkableObject = true;
        willActivateWhenMovedTo = false;
        isActivated = true;

        if (StateManager.gameState == StateManager.GameState.IN_GAME)
        {
            Vector3 spawnPosition = new Vector3(tile.transform.position.x, 1, tile.transform.position.z);
            GameObject spawnedPlayer = Instantiate(playerPrefab.gameObject, spawnPosition, Quaternion.identity) as GameObject;

            //set up the player
            spawnedPlayer.GetComponent<Player>().Init(tile);

            //remove the playerStart from this tile and put the player in is place
            tile.ObjectOnTile = spawnedPlayer.gameObject;
            this.gameObject.SetActive(false);
        }

        currentTile = tile;
    }

    public override PlaceableObjectData GenerateDataClass()
    {
        return new PlayerStartData(isWalkableObject, willActivateWhenMovedTo, isActivated);
    }
}



[Serializable]
public class PlayerStartData : PlaceableObjectData
{
    public PlayerStartData(bool _isWalkableObject, bool _willActivateWhenMovedTo, bool _isActivated) : base(_isWalkableObject, _willActivateWhenMovedTo, _isActivated)
    {
    }
}