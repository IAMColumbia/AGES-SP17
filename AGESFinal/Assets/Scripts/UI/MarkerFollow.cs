using UnityEngine;
using System.Collections;

public class MarkerFollow : MonoBehaviour {

    //A script that displays the player markers above their sprites. adds a nice damp effect for aesthetics
    [SerializeField]
    private Transform playerToFollow;
    [SerializeField]
    private float dampTime = .05f;

    private Vector2 moreVelocity;

    private void Start()
    {
        transform.parent = null;
    }
    void Update () {

        transform.position = Vector2.SmoothDamp(transform.position, playerToFollow.position, ref moreVelocity, dampTime);

        if (!playerToFollow.transform.parent.gameObject.activeSelf)
            Destroy(gameObject);
	}
}
