using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableBlock : PlaceableObject, IActivatable, IMovable
{
    [SerializeField] RectTransform BlockCanvas;

    //I DON'T WANT THE BLOCK TO HAVE A REFERENCE TO THE BOARD ORRRR PLAYER!!! :'(
    Board board;
    Player player;

    //this should be called when the player tries to move onto a space with a block
    public void Activate(GameObject ObjectActivatedBy)
    {
        player = FindObjectOfType<Player>();        //NOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO

        //if the gameObject is a player (and we're not already interacting with something)...
        if (ObjectActivatedBy.GetComponent<Player>() && PlaceableObject.currentObjectInteractedWith == null)
        {
            //show UI for push/pull blocks
            BlockCanvas.gameObject.SetActive(true);

            //get the direction the player is from the block
            Vector3 originalRotation = BlockCanvas.localEulerAngles;
            BlockCanvas.transform.forward = ObjectActivatedBy.transform.forward;

            //if the player clicks the move forward / move backwards arrow, check to see if the block can be moved (is the next space walkable?)

            //if player clicks on a space, unselect this block is not currently being pushed
            currentObjectInteractedWith = this;
            StateManager.playerState = StateManager.PlayerState.INTERACTING;

        }
    }

    public override void SetUpObjectOnGameStart(Tile tile)
    {
        isWalkableObject = false;
        willActivateWhenMovedTo = true;
        isActivated = true;

        //Set up the rest of block
        board = FindObjectOfType<Board>();  //NOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO
        currentTile = tile;

        if (StateManager.gameState == StateManager.GameState.IN_GAME)
        {
            //When in game, a block needs to be considered a character, not a gameObject
            tile.ObjectOnTile = null;
            tile.CharacterOnTile = this.gameObject;
        }
    }

    public override void DeselectInteractableObject()
    {
        BlockCanvas.gameObject.SetActive(false);
        PlaceableObject.currentObjectInteractedWith = null;
        StateManager.playerState = StateManager.PlayerState.WALKING;
    }

    public void MoveForward()
    {
        //this rotation saving is kinda janky (force player rotation)
        Quaternion originalRotation = player.transform.rotation;
        if (board.AttemptMoveTargetToTile(this.gameObject, BlockCanvas.forward)) //the blockCanvas is rotated in the correct direction, so we'll use that instead of the block's rotation
        {
            board.AttemptMoveTargetToTile(player.gameObject, BlockCanvas.forward);
            player.transform.rotation = originalRotation;
        }

        //check if the block can move forward
        //move the block forward
        //move the player forward
    }


    //TODO: PART OF THIS PROBLEM MAY BE THAT: BlockCanvas.forward isn't EXACTLY 1, which is a problem!
    //TODO: FORWARD VECTOR ISN'T ALWAYS: up, down, left, right.  Sometimes it's (1, 0 , 1), not (1, 0, 0)
    //DIAGONAL PROBLEM
    public void MoveBackward()
    {
        //this rotation saving is kinda janky (force player rotation)
        Quaternion originalRotation = player.transform.rotation;
        if (board.AttemptMoveTargetToTile(player.gameObject, -BlockCanvas.forward)) //the blockCanvas is rotated in the correct direction, so we'll use that instead of the block's rotation
        {
            board.AttemptMoveTargetToTile(this.gameObject, -BlockCanvas.forward);
            player.transform.rotation = originalRotation;
        }

        //check if the player can move backward
        //move the player backward
        //move the block backward
    }


    public bool GoToTile(Tile tile)
    {
        tile.AttemptActivateObject(this.gameObject);

        if (tile.isWalkable)
        {
            Vector3 targetPosition = new Vector3(tile.transform.position.x, this.transform.position.y, tile.transform.position.z);
            transform.position = targetPosition;

            //When finishing moving an object, set the previous tile's object to null, then change the the objectOnTile to this gameobject
            currentTile.CharacterOnTile = null;
            //currentTile.ObjectOnTile = null;
            currentTile = tile;
            //currentTile.ObjectOnTile = this.gameObject;
            currentTile.CharacterOnTile = this.gameObject;


            //TODO FROM ABOVE /\: Pushable block should be considered a "Character" for my collision, but but considered an "ObjectOnTile" for interaction
            return true;
        }

        return false;
    }

    public Tile GetCurrentTile()
    {
        return currentTile;
    }

    public override PlaceableObjectData GenerateDataClass()
    {
        return new PushableBlockData(isWalkableObject, willActivateWhenMovedTo, isActivated);
    }
}




[Serializable]
public class PushableBlockData : PlaceableObjectData
{
    public PushableBlockData(bool _isWalkableObject, bool _willActivateWhenMovedTo, bool _isActivated) : base(_isWalkableObject, _willActivateWhenMovedTo, _isActivated)
    {
    }
}