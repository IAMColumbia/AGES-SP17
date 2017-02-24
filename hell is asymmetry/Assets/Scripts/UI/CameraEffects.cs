using UnityEngine;
using System.Collections;

public class CameraEffects : MonoBehaviour {

    [SerializeField]
    Camera cameraA, cameraB;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.O))
        {
            toggleCameraClearFlags(cameraA);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            toggleCameraClearFlags(cameraB);
        }
	}

    void toggleCameraClearFlags(Camera camera)
    {
        if (camera.clearFlags == CameraClearFlags.SolidColor)
        {
            camera.clearFlags = CameraClearFlags.Depth;
        }
        else
        {
            camera.clearFlags = CameraClearFlags.SolidColor;
        }
    }
}
