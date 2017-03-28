using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ControllerInputForMenus : MonoBehaviour
{
    //this script was taken and modified from the main menu tutorial
    public EventSystem eventSystem;
    public GameObject selectedObject;

    private bool buttonSelected;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("VerticalUI") != 0 && buttonSelected == false)
        {
            eventSystem.SetSelectedGameObject(selectedObject);
            buttonSelected = true;
        }
    }

    private void OnDisable()
    {
        buttonSelected = false;
    }
}

