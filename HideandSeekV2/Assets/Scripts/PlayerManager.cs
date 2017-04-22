using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerManager : MonoBehaviour {

    
    public int m_PlayerNumber;            // This specifies which player this the manager for.
    [HideInInspector]
    public string m_ColoredPlayerText;    // A string that represents the player with their number colored to match their tank.
    
    public GameObject m_Instance;         // A reference to the instance of the tank when it is created.
    public Transform m_SpawnPoint;                          // The position and direction the tank will have when it spawns.
    RigidbodyFirstPersonController m_Movement;
    public Camera PlayerCam;
    public LayerMask mask;

    public void Setup()
    {
        m_Movement = m_Instance.GetComponent<RigidbodyFirstPersonController>();

        m_Movement.m_PlayerNumber = m_PlayerNumber;

        PlayerCam = m_Movement.GetComponentInChildren<Camera>();

        if (m_Instance.name == "Player1")
        {
            m_Instance.gameObject.layer = LayerMask.NameToLayer("Player1"); // Sets Player Layer
            PlayerCam.cullingMask ^= 1 << LayerMask.NameToLayer("Player1"); // Sets Camera Culling Mask
            PlayerCam.rect = new Rect( .51f,.52f,.49f,48f); // Sets Camera Viewport
            m_Instance.gameObject.tag = "Seeker"; // Sets Tag of Player
        }

        if (m_Instance.name == "Player2")
        {
            m_Instance.gameObject.layer = LayerMask.NameToLayer("Player2");
            PlayerCam.cullingMask ^= 1 << LayerMask.NameToLayer("Player2");
            PlayerCam.rect = new Rect(0f,.52f,.49f,.48f);
            m_Instance.gameObject.tag = "Hider";
           m_Instance.GetComponentInChildren<AudioListener>().enabled = false;
        }

        if (m_Instance.name == "Player3")
        {
            m_Instance.gameObject.layer = LayerMask.NameToLayer("Player3"); 
            PlayerCam.cullingMask ^= 1 << LayerMask.NameToLayer("Player3");
            PlayerCam.rect = new Rect(0f,0f,.49f,.48f);
            m_Instance.gameObject.tag = "Hider";
            m_Instance.GetComponentInChildren<AudioListener>().enabled = false;
        }

        if (m_Instance.name == "Player4")
        {
            m_Instance.gameObject.layer = LayerMask.NameToLayer("Player4");
            PlayerCam.cullingMask ^= 1 << LayerMask.NameToLayer("Player4");
            PlayerCam.rect = new Rect(.51f,0f,.49f,.48f);
            m_Instance.gameObject.tag = "Hider";
            m_Instance.GetComponentInChildren<AudioListener>().enabled = false;
        }

    }
}
