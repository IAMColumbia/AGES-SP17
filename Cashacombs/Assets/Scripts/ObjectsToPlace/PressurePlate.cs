using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : PlaceableObject, IActivatable
{
    public LayerMask tileLayer;
    public bool ActsAsOnOffSwitch;      //TODO: Make this its own placeableObject (one for On/Off Lever-type, another for PressurePlate)
    public bool StartsDeactivated;        //TODO NOTE: This is for PressurePlate, NOT On/Off
    PlaceableObject ObjectToTrigger;

    Vector2 targetTileIndex;
    bool selectingTargetObject = false;
    LineRenderer lineRenderer;

    //Shader currentlyHoveredGameObjectShader;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        if(lineRenderer == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
            lineRenderer.widthMultiplier = 0.1f;
            lineRenderer.enabled = false;
        }
    }

    public void Activate(GameObject ObjectActivatedBy)
    {
        Debug.Log("Activated Pressure Plate");
        //turn the connectedGameObject On or Off

        if (ObjectToTrigger)
        {
            if (ActsAsOnOffSwitch)
            {
                ObjectToTrigger.isActivated = !ObjectToTrigger.isActivated;

                //TEMP CODE: (replace when the object visually changes when it's on or off)
                ObjectToTrigger.gameObject.SetActive(!ObjectToTrigger.gameObject.activeSelf);
            }
        }

    }

    public override void SetUpObjectOnGameStart(Tile tile)
    {
        isActivated = true;
        isWalkableObject = true;
        willActivateWhenMovedTo = true;

        currentTile = tile;
    }

    void SetObjectToTrigger(PlaceableObject targetObject)
    {
        if(targetObject != this)
            ObjectToTrigger = targetObject;
    }

    private void Update()
    {
        AdjustLine();

        //TODO: Only call this when the player has moved!!!! (There's no need to call this every frame!)
        if (!ActsAsOnOffSwitch && StateManager.gameState == StateManager.GameState.IN_GAME)
        {
            if (ObjectToTrigger && currentTile.CharacterOnTile)
            {
                ObjectToTrigger.isActivated = StartsDeactivated;
                ObjectToTrigger.gameObject.SetActive(StartsDeactivated); //TODO: temp code (replace visually)
            }
            else if (ObjectToTrigger && !currentTile.CharacterOnTile)
            {
                ObjectToTrigger.isActivated = !StartsDeactivated;
                ObjectToTrigger.gameObject.SetActive(!StartsDeactivated); //TODO: temp code (replace visually)
            }
        }
    }

    public override void LateSetup(Tile objectToSetup)
    {
        ObjectToTrigger = objectToSetup.ObjectOnTile.GetComponent<PlaceableObject>();
    }

    void AdjustLine()
    {
        if (StateManager.gameState == StateManager.GameState.LEVEL_EDITOR)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                //if your mouse is has clicked on THIS pressureplate
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider == this.GetComponent<BoxCollider>())
                    {
                        lineRenderer.enabled = true;
                        selectingTargetObject = true;

                        //Reset the line to prevent an incorrect, one-frame line
                        Vector3 mousePositionInWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        lineRenderer.SetPositions(new Vector3[] { transform.position, mousePositionInWorld });
                    }
                }
            }
            else if (Input.GetKey(KeyCode.Mouse0) && selectingTargetObject)
            {
                Vector3 mousePositionInWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                lineRenderer.SetPositions(new Vector3[] { transform.position, mousePositionInWorld });


                //highlight object if it can be connected
                //PlaceableObject potentialPlacableObject = GetHoveredPlacableObject();
                //if (potentialPlacableObject)
                //{
                    //Thanks to Nick-Wiggill for the shader!!!! https://forum.unity3d.com/threads/solved-gameobject-picking-highlighting-and-outlining.40407/

                    //reset the currently hovered gameObject's shader back to Diffuse, update that same variable, then change the shader to the highlight
                    //currentlyHoveredGameObjectShader = Shader.Find("Diffuse");
                    //currentlyHoveredGameObjectShader = potentialPlacableObject.gameObject.GetComponent<Renderer>().material.shader;
                    //currentlyHoveredGameObjectShader = Shader.Find("Self-Illumin/Outlined Diffuse");
                //}
            }
            else if (Input.GetKeyUp(KeyCode.Mouse0) && selectingTargetObject)
            {
                //if(currentlyHoveredGameObjectShader)
                //    currentlyHoveredGameObjectShader = Shader.Find("Self-Illumin/Outlined Diffuse");

                SetObjectToTrigger(GetHoveredPlacableObject());
                selectingTargetObject = false;
                lineRenderer.enabled = false;
            }
        }
    }

    PlaceableObject GetHoveredPlacableObject()
    {
        //because the majority of placeable objects don't have colliders, I have to find the tile instead

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, 999, tileLayer))
        {
            Tile targetTile = hit.collider.GetComponent<Tile>();
            if(targetTile)
            {
                if(targetTile.ObjectOnTile != null)
                {
                    return targetTile.ObjectOnTile.GetComponent<PlaceableObject>();
                }
            }
        }

        //if (Physics.Raycast(ray, out hit))
        //{
        //    PlaceableObject possibleTarget = hit.collider.gameObject.GetComponent<PlaceableObject>();
        //    if (possibleTarget)
        //    {
        //        return possibleTarget;
        //    }
        //}

        return null;
    }

    public override PlaceableObjectData GenerateDataClass()
    {
        return new PressurePlateData(isWalkableObject, willActivateWhenMovedTo, isActivated, ObjectToTrigger.currentTile.tileRowColumnIndex);
    }
}






[Serializable]
public class PressurePlateData : PlaceableObjectData
{
    public int row, column;

    public PressurePlateData(bool _isWalkableObject, bool _willActivateWhenMovedTo, bool _isActivated, Vector2 _connectedTileIndex) : base(_isWalkableObject, _willActivateWhenMovedTo, _isActivated)
    {
        //LEFT OFF: currentTile cannot be passed, since currentTile is only setup when the game starts
        row = (int)_connectedTileIndex.x;
        column = (int)_connectedTileIndex.y;
    }
}