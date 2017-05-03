using UnityEngine;
using System.Collections;

public class CameraPosition : MonoBehaviour {

    //these are the animation trigger names
    string[] cameraPositions =
    {
        //"Center",
        "Sonar",
        "Engine",
        "Torpedo"
    };

    int cameraPositionIndex = -1;

    [SerializeField]
    Animator m_animator;

    [SerializeField]
    Canvas torpedoUI; //temporary until screens actually look good

	// Use this for initialization
	void Start () {
        nextCameraPosition();
	}
	
	// Update is called once per frame
	void Update () {
        if (!m_animator.IsInTransition(0))
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                nextCameraPosition();
                //updateTorpedoUI();
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                prevCameraPosition();
                //updateTorpedoUI();
            }
        }
	}

    void nextCameraPosition()
    {
        cameraPositionIndex++;

        if (cameraPositionIndex >= cameraPositions.Length) { cameraPositionIndex = 0; }

        m_animator.SetTrigger(cameraPositions[cameraPositionIndex]);
    }

    void prevCameraPosition()
    {
        cameraPositionIndex--;

        if (cameraPositionIndex < 0) { cameraPositionIndex = cameraPositions.Length - 1; }

        m_animator.SetTrigger(cameraPositions[cameraPositionIndex]);
    }

    void updateTorpedoUI()
    {
        if(cameraPositions[cameraPositionIndex] == "Torpedo")
        {
            torpedoUI.gameObject.SetActive(true);
        }
        else
        {
            torpedoUI.gameObject.SetActive(false);
        }
    }
}
