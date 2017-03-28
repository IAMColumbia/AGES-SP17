using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ApplyPlayerColorsAtStart : MonoBehaviour
{
    //this script was taken and modified from the wheel collider tank tutorial
    [SerializeField]
    private Color colorToApply;
    [SerializeField]
    private Text textToColor;

	// Use this for initialization
	void Start ()
    {
        MeshRenderer[] renders = GetComponentsInChildren<MeshRenderer>();
        textToColor.color = colorToApply;

        for (int i = 0; i < renders.Length; i++)
        {
            renders[i].material.color = colorToApply;
        }

        
	
	}
}
