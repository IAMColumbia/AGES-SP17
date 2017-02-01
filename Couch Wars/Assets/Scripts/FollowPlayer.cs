using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour
{
	public Vector3 offset;			// The offset at which the Health Bar follows the player.
    public GameObject playerToFollow;

    private Transform player;

	void Awake ()
	{
		// Setting up the reference.
		player = playerToFollow.transform;
	}

	void Update ()
	{
		// Set the position to the player's position with the offset.
		transform.position = player.position + offset;
	}
}
