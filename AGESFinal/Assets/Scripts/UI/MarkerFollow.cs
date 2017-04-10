using UnityEngine;
using System.Collections;

public class MarkerFollow : MonoBehaviour {

    //A script that displays the player markers above their sprites. adds a nice damp effect for aesthetics
    public Transform playerToFollow;

    private Vector2 moreVelocity;
    private float dampTime = .05f;

    void Update () {

        transform.position = Vector2.SmoothDamp(transform.position, playerToFollow.position, ref moreVelocity, dampTime);
	}
}
