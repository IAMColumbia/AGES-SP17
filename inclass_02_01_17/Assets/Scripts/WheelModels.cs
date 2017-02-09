using UnityEngine;
using System.Collections;

public class WheelModels : MonoBehaviour {

    public WheelCollider[] WheelColliders;
    public Transform[] WheelMeshes;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 position;
        Quaternion rotation;
	    for(int i= 0; i < WheelColliders.Length; i++)
        {
            WheelColliders[i].GetWorldPose(out position, out rotation);

            WheelMeshes[i].position = position;
            WheelMeshes[i].rotation = rotation;
        }
	}
}
