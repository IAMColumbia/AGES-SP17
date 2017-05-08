using UnityEngine;
using System.Collections;
using System;

public class CameraFollowScript : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private float followDistance;

    private Vector3 newCameraPosition;

	// Update is called once per frame
	void Update ()
    {
        UpdateFollowPosition();
        FollowPlayer();
    }

    private void UpdateFollowPosition()
    {
        newCameraPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, (player.transform.position.z - followDistance));
    }

    private void FollowPlayer()
    {
        gameObject.transform.position = newCameraPosition;
    }
}
