using System;
using UnityEngine;

[Serializable]
public class TankManager
{
    public Color m_PlayerColor;            
    public Transform m_SpawnPoint;         
    [HideInInspector] public int m_PlayerNumber;             
    [HideInInspector] public string m_ColoredPlayerText;
    [HideInInspector] public GameObject m_Instance;          
    [HideInInspector] public int m_Wins;


    private TankMovement m_Movement;       
    private TankShooting m_Shooting;
    //private DeadTank deadTank;
    private GameObject m_CanvasGameObject;


    public void Setup()
    {
        m_Movement = m_Instance.GetComponent<TankMovement>();
        m_Shooting = m_Instance.GetComponent<TankShooting>();
        //deadTank = m_Instance.GetComponent<DeadTank>();
        m_CanvasGameObject = m_Instance.GetComponentInChildren<Canvas>().gameObject;

        m_Movement.PlayerNumber = m_PlayerNumber;
        m_Shooting.PlayerNumber = m_PlayerNumber;
        //deadTank.PlayerNumber = m_PlayerNumber;

        m_ColoredPlayerText = "<color=#" + ColorUtility.ToHtmlStringRGB(m_PlayerColor) + ">PLAYER " + m_PlayerNumber + "</color>";

        MeshRenderer[] renderers = m_Instance.GetComponentsInChildren<MeshRenderer>();

        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material.color = m_PlayerColor;
        }
    }


    public void DisableControl()
    {
        m_Movement.enabled = false;
        m_Shooting.enabled = false;
        //deadTank.enabled = false;

        m_CanvasGameObject.SetActive(false);
    }


    public void EnableControl()
    {
        m_Movement.enabled = true;
        m_Shooting.enabled = true;

        //deadTank.enabled = false;

        m_CanvasGameObject.SetActive(true);
    }

    public void EnableDeadTank()
    {
        m_Movement.enabled = true;
        //deadTank.enabled = true;

        m_Shooting.enabled = false;
    }


    public void DisableShootingOnly()
    {
        m_Shooting.enabled = false;
        //deadTank.enabled = false;
    }


    public void Reset()
    {
        m_Instance.transform.position = m_SpawnPoint.position;
        m_Instance.transform.rotation = m_SpawnPoint.rotation;
        m_Instance.GetComponent<TankShooting>().ReloadTank();

        m_Instance.SetActive(false);
        m_Instance.SetActive(true);
    }
}
