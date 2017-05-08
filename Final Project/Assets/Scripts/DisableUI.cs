using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DisableUI : MonoBehaviour {

    [SerializeField]
    GameObject panel;

	// Use this for initialization
	void Awake ()
    {
        panel.SetActive(true);
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if (Input.anyKey)
        {
            Debug.Log(panel.ToString() + " has been deactivated");
            panel.SetActive(false);
        }
	}
}
