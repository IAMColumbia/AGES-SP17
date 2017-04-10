﻿using UnityEngine;
using System.Collections;

public class SwitchWorlds : MonoBehaviour
{
    [SerializeField]
    Transform world2Position;
    [SerializeField]
    Transform world1Position;

    bool isInWorld2;
	// Use this for initialization
	void Start ()
    {
        isInWorld2 = false;
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(!isInWorld2)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                this.transform.position = world2Position.position;
                isInWorld2 = true;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                this.transform.position = world1Position.position;
                isInWorld2 = false;
            }
        }
	}
}
