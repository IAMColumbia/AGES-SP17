using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class DragObject : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform parentReturn;

    public static GameObject draggedObject;
    Vector3 startPosition;

    public void OnBeginDrag(PointerEventData eventData)
    {
        parentReturn = transform.parent;
        transform.SetParent(transform.parent.parent);
        GetComponent<CanvasGroup>().blocksRaycasts = false;

        //transform.parent.parent.parent.SetSiblingIndex(4);
        transform.parent.SetAsLastSibling();

        /*draggedObject = gameObject;
        startPosition = transform.position;
        parentReturn = transform.parent;
        GetComponent<CanvasGroup>().blocksRaycasts = false;*/
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

        /*draggedObject = null;

        if (transform.parent != parentReturn)
        {
            transform.position = startPosition;
        }

        GetComponent<CanvasGroup>().blocksRaycasts = true;*/
    }
}
/*public void BeginDrag()
	{
        if (DraggingEnabled)
        {
            ParentReturn = transform.parent;
		    transform.SetParent(transform.parent.parent);
		    GetComponent<CanvasGroup>().blocksRaycasts = false;

            transform.parent.parent.parent.SetSiblingIndex(4);
            transform.parent.SetAsLastSibling();
        }
    }

	public void Drag()
	{
        if (DraggingEnabled)
        {
            transform.position = Input.mousePosition;
        }
    }

	public void EndDrag()
	{
        if (DraggingEnabled)
        {
            transform.SetParent(ParentReturn);
            GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
    }
*/