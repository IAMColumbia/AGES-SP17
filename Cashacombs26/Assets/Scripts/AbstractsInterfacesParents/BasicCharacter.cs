using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicCharacter : MonoBehaviour, IMovable
{
    /* THE PURPOSE OF THIS CLASS IS TO: Provide a framework for any character in the game
     * Characters include:
     *      - Players
     *      - Monsters
     *      - Any objects that can move, die, AND activate objects (pushableBlocks not included)
     */

    Rigidbody myRigidbody;
    Tile currentTile;

    protected bool canDie = true;

    // Use this for initialization
    public virtual void Init(Tile startingTile)
    {
        myRigidbody = GetComponent<Rigidbody>();
        currentTile = startingTile;
    }

    public virtual void Kill()
    {
        //kill the character, override in subclasses
    }



    public bool GoToTile(Tile tile)
    {
        //rotate towards the tile
        transform.LookAt(new Vector3(tile.transform.position.x, transform.position.y, tile.transform.position.z));

        tile.AttemptActivateObject(this.gameObject);

        if (tile.isWalkable)
        {
            //move to that tile (maybe don't use rigidbodies.  I may not need them?)
            Vector3 targetPosition = new Vector3(tile.transform.position.x, this.transform.position.y, tile.transform.position.z);
            myRigidbody.MovePosition(targetPosition);


            //When finishing moving an object, set the previous tile's object to null, then change the the objectOnTile to this gameobject
            currentTile.CharacterOnTile = null;
            currentTile = tile;
            currentTile.CharacterOnTile = this.gameObject;
            return true;
        }

        return false;
    }

    public Tile GetCurrentTile()
    {
        return currentTile;
    }
}
