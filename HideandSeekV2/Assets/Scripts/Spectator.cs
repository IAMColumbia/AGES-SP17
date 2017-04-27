using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class Spectator : MonoBehaviour {

    public Camera SpectatorCam;
    RigidbodyFirstPersonController RFPC;
    PlayerManager PM;
    Canvas updatedCanvas;
    GameManager GM;
    

    void Start ()
    {
        GM = FindObjectOfType<GameManager>();
        PM = GetComponent<PlayerManager>();
        //updatedCanvas = PM.m_Instance.GetComponent<PlayerManager>().playerCanvas;
        RFPC = GetComponent<RigidbodyFirstPersonController>();
        //updatedCanvas.worldCamera = SpectatorCam;
        SpecSetup();
    }
    void AddSpectatorCam ()
    {
        
        SpectatorCam = this.gameObject.AddComponent<Camera>();
        SpectatorCam.rect = RFPC.cam.rect;
       // SpectatorCam.GetComponent<AudioListener>().enabled = false;
        RFPC.cam.enabled = false;

    }

    public void SpecSetup ()
    {

        AddSpectatorCam();
    }




}
