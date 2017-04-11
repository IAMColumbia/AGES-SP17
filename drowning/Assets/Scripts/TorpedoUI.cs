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

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        float scrollAmount = Input.GetAxis("Mouse ScrollWheel");

        ATheta += scrollAmount * scrollSpeed;

        if(ATheta > 180) { ATheta = -180; }
        if(ATheta < -180) { ATheta = 180; }

        headingCircle.eulerAngles = new Vector3(0, 0, -ATheta);

        angleText.text = Mathf.Floor(ATheta).ToString();

        torpedoInformationPanel.verticalScrollbar.value += scrollAmount;
	}


    public void Fire()
    {
        Debug.Log("firing torpedo from tube A; heading is " + ATheta);
        torpedoLauncher.LaunchTorpedo(ATheta);
    }
}
