using UnityEngine;
using System.Collections;

// PlayerScript requires the GameObject to have a Rigidbody2D component

[RequireComponent(typeof(Rigidbody))]

public class Player : MonoBehaviour
{
    public enum PlayerState { Safe, InDanger };

    private PlayerState curPlayerState;

    private CameraController camera;

    private void Start()
    {
        curPlayerState = PlayerState.Safe;
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.tag == "NPCCamera")
        {
            curPlayerState = PlayerState.InDanger;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.tag == "NPCCamera")
        {
            curPlayerState = PlayerState.Safe;
        }
    }

    public bool IsPlayerScrewed()
    {
        if (curPlayerState == PlayerState.InDanger)
        {
            return true;
        }

        else
        {
            return false;
        }
    }
}