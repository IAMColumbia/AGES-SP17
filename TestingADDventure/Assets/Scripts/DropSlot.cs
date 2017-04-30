using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class DropSlot : MonoBehaviour, IDropHandler
{
    /*public GameObject droppedObject
    {
        get
        {
            if (transform.childCount > 0)
            {
                return transform.GetChild(0).gameObject;
            }
            else
            {
                return null;
            }
        }
    }*/

    public void OnDrop(PointerEventData eventData)
    {
        DragObject d = eventData.pointerDrag.GetComponent<DragObject>();

        d.parentReturn = transform;
        d.transform.position = transform.position;
        /*if (!droppedObject)
        {
            DragObject.draggedObject.transform.SetParent(transform);
        }
        else
        {
            Transform swap = DragObject.draggedObject.transform.parent;
            DragObject.draggedObject.transform.SetParent(transform);
            droppedObject.transform.SetParent(swap);
        }*/
    }
}
/*ObjectDrag d = eventData.pointerDrag.GetComponent<ObjectDrag>();

        if (d.ItemType == SlotType && d.ItemSubType == SlotNumber)
        {
            d.ParentReturn = transform;
        }
*/