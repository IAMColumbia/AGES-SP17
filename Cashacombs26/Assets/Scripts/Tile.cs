using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* THE PURPOSE OF THIS CLASS IS TO: Keep track of this tile/space
 * 1) What is on this space? (The player?  A monster?  A block?  A trap?  Nothing at all?)
 * 2) What traits does this space have (Can it be walked on?  Is it activated?)
 * 3) Triggering things that pass-through / step-on this block
 */

public class Tile : MonoBehaviour
{
    #region Properties
    public bool isWalkable //can players and monsters walk on this tile?
    {
        get
        {
            if (CharacterOnTile != null && CharacterOnTile.activeSelf) //return false if a character is on the tile
            {
                return false;
            }
            if (ObjectOnTile != null)
            {
                if (ObjectOnTile.GetComponent<PlaceableObject>() != null) //if there's a placeableObject on the tile...
                {
                    //return true if the tile is walkable or if the gameObject is SetActive(false)
                    return ObjectOnTile.GetComponent<PlaceableObject>().isWalkableObject || !ObjectOnTile.activeSelf;
                }
            }

            return true;
        }
    }

    public bool isActivatable   //if a character tries to move onto this tile, will it be activated?
    {
        get
        {
            if (ObjectOnTile != null && ObjectOnTile.GetComponent<PlaceableObject>() != null)
            {
                //return true if the object is meant to be activated with and if the object is currently active
                return ObjectOnTile.GetComponent<PlaceableObject>().willActivateWhenMovedTo && ObjectOnTile.GetComponent<PlaceableObject>().isActivated;
            }

            return false;
        }
    }


    public Vector2 tileRowColumnIndex
    {
        get { return tilePosition; }
    }
    #endregion

    public GameObject ObjectOnTile;     //what is on this tile?  Player?  Block?  Trap?  Nothing?  Set in "SetItemToTile," which is called from Conroller
    public GameObject CharacterOnTile;

    Vector2 tilePosition;

    public void GameInit(Vector2 tilePos)
    {
        //if the tile contains an object with an IPlaceableObject attached...
        if (ObjectOnTile && ObjectOnTile.GetComponent<PlaceableObject>() != null)
        {
            //Set up the object
            PlaceableObject objectScript = ObjectOnTile.GetComponent<PlaceableObject>();
            objectScript.SetUpObjectOnGameStart(this);
        }

        tilePosition = tilePos;
    }

    public void Init(Vector2 tilePos)
    {
        tilePosition = tilePos;
    }

    /// <summary>
    /// Place the desiredObject on the tile if there is not already an object there
    /// </summary>
    /// <param name="desiredObject"></param>
    /// <returns>Returns the GameObject on the tile. Returns the old one if there was already an object there.</returns>
    public GameObject SetItemToTile(GameObject desiredObject)
    {
        if (ObjectOnTile == null)
        {
            ObjectOnTile = Instantiate(desiredObject, transform.position, Quaternion.identity);
            ObjectOnTile.GetComponent<PlaceableObject>().SetUpObjectOnGameStart(this);
        }

        return ObjectOnTile;
    }

    public void RemoveObjectFromTile()
    {
        Destroy(ObjectOnTile);
        ObjectOnTile = null;
    }

    public void DestroySelf()
    {
        //if there's on object on the tile, destroy it
        if (ObjectOnTile)
        {
            Destroy(ObjectOnTile);
        }

        if (CharacterOnTile)
        {
            Destroy(CharacterOnTile);
        }

        Destroy(this.gameObject);
    }




    public void AttemptActivateObject(GameObject ObjectActivatedBy)
    {
        //if we can activate things and the gameObject on this tile implements the IActivatable interface...

        //check for characters first
        if (CharacterOnTile != null && CharacterOnTile.GetComponent<IActivatable>() != null)
        {
            CharacterOnTile.GetComponent<IActivatable>().Activate(ObjectActivatedBy);
        }
        //check for objects (NOTE: if you always want to check both, remove "else")
        else if (isActivatable && ObjectOnTile.GetComponent<IActivatable>() != null)
        {
            ObjectOnTile.GetComponent<IActivatable>().Activate(ObjectActivatedBy);
        }


    }

    //this chains from CONTROLLER -> TILE -> PLACABLE-OBJECT (could easily go controller -> placableObject)
    public static void DeselectCurrentObject()
    {
        PlaceableObject.currentObjectInteractedWith.DeselectInteractableObject();
    }




    #region Saving and Loading

    public TileData GenerateTileData()
    {
        if (ObjectOnTile != null)
        {
            return new TileData(ObjectOnTile.GetComponent<PlaceableObject>().GenerateDataClass(), tileRowColumnIndex);
        }

        return new TileData(null, tileRowColumnIndex);
    }

    #endregion
}






[Serializable]
public class TileData
{
    public PlaceableObjectData prefabOnTileName;
    int row, column;

    public TileData(PlaceableObjectData objectOnTile, Vector2 location)
    {
        if (objectOnTile != null)
        {
            prefabOnTileName = objectOnTile;
        }
        else
        {
            prefabOnTileName = null;
        }


        row = (int)location.x;
        row = (int)location.y;
    }
}