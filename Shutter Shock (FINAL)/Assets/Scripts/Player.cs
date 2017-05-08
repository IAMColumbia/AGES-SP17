using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

// PlayerScript requires the GameObject to have a Rigidbody2D component

[RequireComponent(typeof(Rigidbody))]

public class Player : MonoBehaviour
{
    public enum PlayerState { Safe, InDanger };

    [SerializeField] Text EText;

    private PlayerState curPlayerState;


    private void Start()
    {
        curPlayerState = PlayerState.Safe;
        EText.enabled = false;
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.tag == "NPCCamera")
        {
            curPlayerState = PlayerState.InDanger;
        }

        if (collision.tag == "Door")
        {
            EText.enabled = true;

            if (Input.GetButtonDown("Door"))
            {
                Debug.Log("Load next scene");
                GameManager.Instance.AdvanceLevel();
            }
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.tag == "NPCCamera")
        {
            curPlayerState = PlayerState.Safe;
        }

        if (collision.tag == "Door")
        {
            EText.enabled = false;
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