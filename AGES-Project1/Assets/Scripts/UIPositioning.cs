using UnityEngine;
using System.Collections;

public class UIPositioning : MonoBehaviour {
    [SerializeField]
    bool useRelativeRotation = true;

    [SerializeField]
    GameObject uiRotationAnchor;
    Quaternion relativeRotation;
	// Use this for initialization
	void Start ()
    {
        relativeRotation = uiRotationAnchor.transform.localRotation;
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(useRelativeRotation)
        {
            transform.rotation = relativeRotation;
        }
	}
}
