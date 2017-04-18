using UnityEngine;
using System.Collections;
using System;

public class ColorModelFromPlayerNumber : MonoBehaviour 
{
    [SerializeField]
    Color colorToApply;
    


    public void ApplyColor(int playerNumber)
    {
        colorToApply = ChooseColorFromPlayerNumber(playerNumber);
        ApplyColor(colorToApply);  
    }

    public void ApplyColor(Color colorToApply)
    {
        MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>();

        // Go through all the renderers...
        for (int i = 0; i < renderers.Length; i++)
        {
            // ... set their material color to the color specific to this player.
            renderers[i].material.color = colorToApply;
        }
    }

    private Color ChooseColorFromPlayerNumber(int playerNumber)
    {
        switch (playerNumber)
        {
            case 1:
                return Color.blue;

            case 2:
                return Color.red;
            case 3:
                return Color.green;                
            case 4:
                return Color.yellow;

            default:
                throw new System.Exception("Unsupported player number.");               
        }
    }
}
