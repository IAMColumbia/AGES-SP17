using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TorpedoInfoPanel : MonoBehaviour {

    [SerializeField]
    Text statusText, coordinateText;

    public void updateCoordinateText(Vector2 position)
    {
        coordinateText.text = "TOR X: " + position.x + " TOR Y: " + position.y;
    }

    public void updateStatus(string status)
    {
        statusText.text = status;
    }
}
