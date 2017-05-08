using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : PlaceableObject, IActivatable
{
    public LayerMask tileLayer;
    public bool ActsAsOnOffSwitch;
    public bool StartsDeactivated;
    PlaceableObject ObjectToTrigger;

    Vector2 targetTileIndex;
    bool selectingTargetObject = false;
    LineRenderer lineRenderer;

    AudioManager audioManager;
    [SerializeField] AudioClip bleepSFX;

    [SerializeField] ParticleSystem connectObjectParticleEffect;

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

                ObjectToTrigger.gameObject.SetActive(!ObjectToTrigger.gameObject.activeSelf);
            }
            audioManager.playSFX(bleepSFX);


            if (ObjectToTrigger)
            {
                GameObject particleEffect1 = Instantiate(connectObjectParticleEffect.gameObject, transform.position + Vector3.up * 2, Quaternion.identity);
                GameObject particleEffect2 = Instantiate(connectObjectParticleEffect.gameObject, ObjectToTrigger.transform.position + Vector3.up * 2, Quaternion.identity);

                Destroy(particleEffect1, connectObjectParticleEffect.main.duration * 2);
                Destroy(particleEffect2, connectObjectParticleEffect.main.duration * 2);
            }
        }

    }

    public override void SetUpObjectOnGameStart(Tile tile)
    {
        isActivated = true;
        isWalkableObject = true;
        willActivateWhenMovedTo = true;

        currentTile = tile;
        audioManager = FindObjectOfType<AudioManager>();

    }

    void SetObjectToTrigger(PlaceableObject targetObject)
    {
        if(targetObject != this)
            ObjectToTrigger = targetObject;
    }

    private void Update()
    {
        AdjustLine();

        if (!ActsAsOnOffSwitch && StateManager.gameState == StateManager.GameState.IN_GAME)
        {
            if (ObjectToTrigger && currentTile.CharacterOnTile)
            {
                ObjectToTrigger.isActivated = StartsDeactivated;
                ObjectToTrigger.gameObject.SetActive(StartsDeactivated);
            }
            else if (ObjectToTrigger && !currentTile.CharacterOnTile)
            {
                ObjectToTrigger.isActivated = !StartsDeactivated;
                ObjectToTrigger.gameObject.SetActive(!StartsDeactivated);
            }
        }
    }

    public override void LateSetup(Tile objectToSetup)
    {
        if (objectToSetup.ObjectOnTile)
        {
            ObjectToTrigger = objectToSetup.ObjectOnTile.GetComponent<PlaceableObject>();
        }
        else
        {
            Debug.Log("The variable ObjectOnTile of Tile has not been assigned");
        }
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
            }
            else if (Input.GetKeyUp(KeyCode.Mouse0) && selectingTargetObject)
            {
                PlaceableObject targetObject = GetHoveredPlacableObject();

                if (targetObject)
                {
                    GameObject particleEffect1 = Instantiate(connectObjectParticleEffect.gameObject, transform.position + Vector3.up * 2, Quaternion.identity);
                    GameObject particleEffect2 = Instantiate(connectObjectParticleEffect.gameObject, targetObject.transform.position + Vector3.up * 2, Quaternion.identity);

                    Destroy(particleEffect1, connectObjectParticleEffect.main.duration * 2);
                    Destroy(particleEffect2, connectObjectParticleEffect.main.duration * 2);
                }

                Debug.Log(targetObject);
                SetObjectToTrigger(targetObject);
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

        return null;
    }

    public override PlaceableObjectData GenerateDataClass()
    {
        if (ObjectToTrigger && ObjectToTrigger.currentTile)
        {
            return new PressurePlateData(isWalkableObject, willActivateWhenMovedTo, isActivated, ObjectToTrigger.currentTile.tileRowColumnIndex);
        }
        else
        {
            return new PressurePlateData(isWalkableObject, willActivateWhenMovedTo, isActivated, new Vector2(-1, -1));
        }
    }
}






[Serializable]
public class PressurePlateData : PlaceableObjectData
{
    public int row, column;

    public PressurePlateData(bool _isWalkableObject, bool _willActivateWhenMovedTo, bool _isActivated, Vector2 _connectedTileIndex) : base(_isWalkableObject, _willActivateWhenMovedTo, _isActivated)
    {
        row = (int)_connectedTileIndex.x;
        column = (int)_connectedTileIndex.y;
    }
}