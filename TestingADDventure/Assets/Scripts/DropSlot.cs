using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class DropSlot : MonoBehaviour, IDropHandler
{
    public bool isBackground;

    public void OnDrop(PointerEventData eventData)
    {
        DragObject d = eventData.pointerDrag.GetComponent<DragObject>();

        if (!isBackground)
        {
            d.parentReturn = transform;
            d.transform.position = transform.position;
        }
        else if (isBackground)
        {
            d.parentReturn = d.startParent;
            d.transform.position = d.startParent.position;
        }
    }
}
