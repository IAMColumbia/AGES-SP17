using UnityEngine;
using System.Collections;

public class SwitchWorlds : MonoBehaviour
{
    [SerializeField]
    Transform world2Position;
	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            this.transform.position = world2Position.position;
        }
	
	}
}
