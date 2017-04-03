using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class SelectOnInput : MonoBehaviour
{
    [SerializeField]
    private EventSystem eventSystem;
    [SerializeField]
    private GameObject selectedObject;

    private bool buttonSelected;
    
	void Start ()
    {
	
	}
	
	void Update ()
    {
	    if (Input.GetAxisRaw("Vertical") != 0 && !buttonSelected)
        {
            eventSystem.SetSelectedGameObject(selectedObject);
            buttonSelected = true;
        }
	}

    void OnDisable()
    {
        buttonSelected = false;
    }
}
