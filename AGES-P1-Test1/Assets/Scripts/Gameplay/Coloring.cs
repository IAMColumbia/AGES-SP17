using UnityEngine;
using System.Collections;

public class Coloring : MonoBehaviour {

    [SerializeField]
    MeshRenderer[] playerColoredPieces;

    [SerializeField]
    MeshRenderer[] decalColoredPieces;

    public Material playerColor;

    [SerializeField]
    Material decalColor; 

	// Use this for initialization
	void Start ()
    {
        for (int i = 0; i < playerColoredPieces.Length; i++)
        {
            playerColoredPieces[i].material = playerColor;
        }

        for (int i = 0; i < decalColoredPieces.Length; i++)
        {
            decalColoredPieces[i].material = decalColor;
        }
    }
}
