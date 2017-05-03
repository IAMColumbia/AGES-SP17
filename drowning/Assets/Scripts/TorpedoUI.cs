using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TorpedoUI : MonoBehaviour {

    float ATheta = 0;

    [SerializeField]
    float scrollSpeed, reloadTime;

    float timeOfLastShot = 0;

    [SerializeField]
    RectTransform headingCircle;

    [SerializeField]
    Text angleText;

    [SerializeField]
    ScrollRect torpedoInformationPanel;

    [SerializeField]
    TorpedoLauncher torpedoLauncher;

    [SerializeField]
    Image fireButtonFill;

    public Transform torpedoInfoList;

    Quaternion headingCircleInitialRotation;
    Vector3 headingCircleRotationAxis;

    bool readyToFire = true;

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

        if (!readyToFire)
        {
            float normalizedFillAmount = (Time.time - timeOfLastShot) / reloadTime;
            normalizedFillAmount = Mathf.Clamp01(normalizedFillAmount);

            fireButtonFill.fillAmount = normalizedFillAmount;

            if(normalizedFillAmount == 1)
            {
                readyToFire = true;
            }
        }
	}

    void startReload()
    {
        readyToFire = false;
        fireButtonFill.fillAmount = 0;
        timeOfLastShot = Time.time;
    }

    public void Fire()
    {
        if (readyToFire)
        {
            Debug.Log("firing torpedo from tube A; heading is " + ATheta);
            torpedoLauncher.LaunchTorpedo(ATheta);
            startReload();
        }
    }
}
