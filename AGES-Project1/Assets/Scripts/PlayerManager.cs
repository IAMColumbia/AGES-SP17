using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using System;


[Serializable]
public class PlayerManager {

    public Transform SpawnZone;
    [HideInInspector]
    public int PlayerNumber;
    [HideInInspector]
    public GameObject Instance;
    [HideInInspector]
    public int Wins;
    [HideInInspector]
    public int KillCount;
    GameObject KilledByWhom;
    

    ThirdPersonUserControl MovementScript;
    public PlayerMagic MagicScript;

    public bool IsAlive;
    // Use this for initialization
    void Start ()
    {
        SetSpawnPoint();
	}
	
	// Update is called once per frame
	void Update ()
    {
        
    }
    public void Setup()
    {
        MovementScript = Instance.GetComponent<ThirdPersonUserControl>();
        MagicScript = Instance.GetComponent<PlayerMagic>();

        MovementScript.PlayerNumber = PlayerNumber;
        MagicScript.PlayerNumber = PlayerNumber;
        

        MeshRenderer[] renderers = Instance.GetComponentsInChildren<MeshRenderer>();

        MagicScript.alive = true;

        KillCount = 0;
    }

    public void DisableCharacterControl()
    {
        MovementScript.enabled = false;
        MagicScript.enabled = false;
    }

    public void EnableCharacterControl()
    {
        MovementScript.enabled = true;
        MagicScript.enabled = true;
    }

    public void Reset()
    {
        
        Instance.transform.position = SpawnZone.position;
        Instance.transform.rotation = SpawnZone.rotation;

        Instance.SetActive(false);
        Instance.SetActive(true);
        MagicScript.playerHealth = 100;
        MagicScript.alive = true;
    }

    public void SetSpawnPoint()
    {
        string num = PlayerNumber.ToString();
        SpawnZone = GameObject.Find("PlayerSpawn" + num).transform;
    }

    public void Respawn()
    {
        if(!MagicScript.alive)
        {
            Debug.Log("Player Died");
            
            Reset();
        }
    }
    
    public void UpdateKillCount()
    {
        KillCount = MagicScript.GetComponent<PlayerMagic>().KillCount;
    }

}
