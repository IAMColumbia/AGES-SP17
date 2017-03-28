using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class SelectOnInput : MonoBehaviour
{
    [SerializeField]
    EventSystem eventSystem;
    [SerializeField]
    GameObject selectedObject;

    bool buttonSelected;
    
	void Start ()
    {
        buttonSelected = false;
    }
	
	void Update ()
    {
	    if (Input.GetAxisRaw("Vertical") != 0 && buttonSelected == false)
        {
            Debug.Log("Got Input");
            eventSystem.SetSelectedGameObject(selectedObject);
            buttonSelected = true;
        }
	}

    void OnDisable()
    {
        Debug.Log("Deselected");
        buttonSelected = false;
    }
}
