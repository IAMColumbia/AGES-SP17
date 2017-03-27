using UnityEngine;
using System;
using System.Collections;

[Serializable]
public class PlayerManager {
    
    public Color ParticleSystemColor;
    public Transform SpawnPoint;
    public Material PlayerMaterial;
    [HideInInspector]
    public float PlayerNumber;
    [HideInInspector]
    public string ColoredPlayerText;
    [HideInInspector]
    public GameObject Instance;
    [HideInInspector]
    public int Wins;


    private PlayerController Movement;
    private PlayerShooting Shooting;
    private GameObject CanvasGameObject;
    
    public void Setup()
    {
        Movement = Instance.GetComponent<PlayerController>();
        Shooting = Instance.GetComponent<PlayerShooting>();
        CanvasGameObject = Instance.GetComponentInChildren<Canvas>().gameObject;

        Movement.PlayerNumber = PlayerNumber;
        Shooting.PlayerNumber = PlayerNumber;
        ColoredPlayerText = "<color=#" + ColorUtility.ToHtmlStringRGB(PlayerMaterial.color) + ">PLAYER " + PlayerNumber + "</color>";

        MeshRenderer[] renderers = Instance.GetComponentsInChildren<MeshRenderer>();

        ParticleSystem[] particleSystems = Instance.GetComponentsInChildren<ParticleSystem>();

        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material = PlayerMaterial;
            
        }
        for (int i = 0; i < particleSystems.Length; i++)
        {
            particleSystems[i].startColor = ParticleSystemColor;

        }
    }

    public void DisableControl()
    {
        Movement.enabled = false;
        Shooting.enabled = false;
        CanvasGameObject.SetActive(false);

        
    }

    public void EnableControl()
    {
        Movement.enabled = true;
        Shooting.enabled = true;
        CanvasGameObject.SetActive(true);
    }

    public void Reset()
    {
        if (Instance != null)
        {
            Instance.transform.position = SpawnPoint.position;
            Instance.transform.rotation = SpawnPoint.rotation;

            Instance.SetActive(false);
            Instance.SetActive(true);
        }

    }

}
