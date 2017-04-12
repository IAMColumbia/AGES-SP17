using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TorpedoUI : MonoBehaviour {

    float ATheta = 0;

    [SerializeField]
    float scrollSpeed;

    [SerializeField]
    RectTransform headingCircle;

    [SerializeField]
    Text angleText;

    [SerializeField]
    ScrollRect torpedoInformationPanel;

    [SerializeField]
    TorpedoLauncher torpedoLauncher;

    public Transform torpedoInfoList;

    Quaternion headingCircleInitialRotation;
    Vector3 headingCircleRotationAxis;

	// Use this for initialization
	void Start () {
        headingCircleInitialRotation = headingCircle.transform.rotation;
        headingCircleRotationAxis = headingCircle.transform.forward;
	}
	
	// Update is called once per frame
	void Update () {
        float scrollAmount = Input.GetAxis("Mouse ScrollWheel");

        ATheta += scrollAmount * scrollSpeed;

        if(ATheta > 180) { ATheta = -180; }
        if(ATheta < -180) { ATheta = 180; }

        headingCircle.rotation = Quaternion.AngleAxis(-ATheta, headingCircleRotationAxis) * headingCircleInitialRotation;

        angleText.text = Mathf.Floor(ATheta).ToString();
	}


    public void Fire()
    {
        Debug.Log("firing torpedo from tube A; heading is " + ATheta);
        torpedoLauncher.LaunchTorpedo(ATheta);
    }
}
