using UnityEngine;
using System.Collections;

public class PlayerAiming : MonoBehaviour {

    public float Range;

    private void Update()
    {
        float h = Input.GetAxis("TurretTurnRight1");
        float xPos = h * Range;
    }
}
