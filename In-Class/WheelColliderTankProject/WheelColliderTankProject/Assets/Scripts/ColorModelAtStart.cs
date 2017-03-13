using UnityEngine;
using System.Collections;

public class ColorModelAtStart : MonoBehaviour 
{
    [SerializeField]
    private Color colorToApply;

    void Start () 
	{
        MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>();

        // Go through all the renderers...
        for (int i = 0; i < renderers.Length; i++)
        {
            // ... set their material color to the color specific to this tank.
            renderers[i].material.color = colorToApply;
        } 
    }
}
