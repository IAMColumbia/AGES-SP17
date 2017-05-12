using System;
using UnityEngine;

[Serializable]
public class RingManager

{
    public Transform m_SpawnPoint;
    [HideInInspector]
    public int m_RingNumber; 
    //[HideInInspector]
    public GameObject m_Instance;
    [HideInInspector]
    public int m_Wins;

    
    

 private GameObject m_CanvasGameObject;
    public void Setup()
    {              
        MeshRenderer[] renderers = m_Instance.GetComponentsInChildren<MeshRenderer>();
            
    }
    public void EnableControl()
    {   
        m_CanvasGameObject.SetActive(true);
    }


    public void Reset()
    {
        m_Instance.transform.position = m_SpawnPoint.position;
        m_Instance.transform.rotation = m_SpawnPoint.rotation;

        m_Instance.SetActive(false);
    

        m_Instance.SetActive(true);      
    
    }
}
