using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class DragObject : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform startParent;
    public Transform parentReturn;

    public static GameObject draggedObject;
    Vector3 startPosition;

    public void OnBeginDrag(PointerEventData eventData)
    {
        parentReturn = transform.parent;
        transform.SetParent(transform.parent.parent);
        GetComponent<CanvasGroup>().blocksRaycasts = false;

        transform.parent.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentReturn);
        transform.position = parentReturn.position;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}