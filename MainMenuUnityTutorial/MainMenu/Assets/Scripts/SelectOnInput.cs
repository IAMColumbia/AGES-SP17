using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class SelectOnInput : MonoBehaviour {

    public EventSystem EventSystem;
    public GameObject SelectedObject;

    bool buttonSelected;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if (Input.GetAxisRaw("Vertical") != 0 && buttonSelected == false)
        {
            EventSystem.SetSelectedGameObject(SelectedObject);
            buttonSelected = true;
        }
	}

    void OnDisable()
    {
        buttonSelected = false;
    }
}
