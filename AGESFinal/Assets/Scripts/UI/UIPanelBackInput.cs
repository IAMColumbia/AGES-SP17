using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIPanelBackInput : MonoBehaviour {
    
	void Update ()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            gameObject.SetActive(false);
            FindObjectOfType<PlayerController>().enabled = true;
        }
	}
}
