using UnityEngine;
using System.Collections;

public class CameraPosition : MonoBehaviour {

    //these are the animation trigger names
    string[] cameraPositions =
    {
        "Center",
        "Sonar",
        "Engine"
    };

    int cameraPositionIndex = 0;

    [SerializeField]
    Animator m_animator;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            cameraPositionIndex++;

            if(cameraPositionIndex >= cameraPositions.Length) { cameraPositionIndex = 0; }

            m_animator.SetTrigger(cameraPositions[cameraPositionIndex]);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            cameraPositionIndex--;

            if (cameraPositionIndex < 0) { cameraPositionIndex = cameraPositions.Length - 1; }

            m_animator.SetTrigger(cameraPositions[cameraPositionIndex]);
        }
	}
}
