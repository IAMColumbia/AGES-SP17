using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class MenuSelect : MonoBehaviour
{
    [SerializeField]
    EventSystem eventSystem;
    [SerializeField]
    GameObject selectedObject;
    [SerializeField]
    string inputAxis;

    bool buttonSelected;

    void Update()
    {
        if (Input.GetAxisRaw(inputAxis) != 0 && !buttonSelected)
        {
            eventSystem.SetSelectedGameObject(selectedObject);
            buttonSelected = true;
        }
    }
}
