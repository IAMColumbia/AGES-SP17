using UnityEngine;
using System.Collections;

public class SetupCursor : MonoBehaviour {

    [SerializeField]
    Texture2D cursorImage;

	// Use this for initialization
	void Start () {
        Cursor.SetCursor(cursorImage, new Vector2(32, 32), CursorMode.Auto);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
