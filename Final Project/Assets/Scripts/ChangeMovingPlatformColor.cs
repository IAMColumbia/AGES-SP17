using UnityEngine;
using System.Collections;
using System;

public class ChangeMovingPlatformColor : MonoBehaviour {

    [SerializeField]
    Material movingPlatformMaterial, stoppedPlatformMaterial;

    [SerializeField]
    Renderer movingPlatformRenderer;

    [SerializeField]
    MovingPlatform movingPlatform;
	void Start ()
    {
        movingPlatformRenderer = GetComponent<Renderer>();
       // movingPlatformRenderer.material.color = movingPlatformColor;
	}
	
	// Update is called once per frame
	void Update ()
    {
        ChangePlatformColor();	
	}

    private void ChangePlatformColor()
    {
        if (movingPlatform.speed > 0)
        {
            movingPlatformRenderer.material.color = movingPlatformMaterial.color;
        }
        else if (movingPlatform.speed <= 0)
        {
            movingPlatformRenderer.material.color = stoppedPlatformMaterial.color;
        }
    }
}
