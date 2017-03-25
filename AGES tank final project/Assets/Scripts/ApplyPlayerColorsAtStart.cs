using UnityEngine;
using System.Collections;

public class ApplyPlayerColorsAtStart : MonoBehaviour
{
    [SerializeField]
    private Color colorToApply;

	// Use this for initialization
	void Start ()
    {
        MeshRenderer[] renders = GetComponentsInChildren<MeshRenderer>();

        for (int i = 0; i < renders.Length; i++)
        {
            renders[i].material.color = colorToApply;
        }
	
	}
}
