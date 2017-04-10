using UnityEngine;
using System.Collections;

public class Camera : MonoBehaviour
{
    [SerializeField]
    GameObject objectToFollow;

    Vector3 offset;
	// Use this for initialization
	void Start ()
    {
        offset = new Vector3();
        offset.z = transform.position.z;
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = objectToFollow.transform.position + offset;
	
	}
}
