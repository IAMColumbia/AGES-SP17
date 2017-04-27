using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{


    public int m_PlayerNumber, numRolled;            // This specifies which player this the manager for.
    [HideInInspector]
    public string m_ColoredPlayerText;    // A string that represents the player with their number colored to match their tank.

    public GameObject m_Instance;         // A reference to the instance of the tank when it is created.
    public Transform m_SpawnPoint;                          // The position and direction the tank will have when it spawns.
   public RigidbodyFirstPersonController m_Movement;
    public Camera PlayerCam;
    public LayerMask mask;
    public bool isSeeker, isHider;
    public Seeker seeker;
    public Hider hider;
    public Spectator spectator;
    GameManager GM;
    AudioListener AL;

    string playername = "Player";
    #region
    [SerializeField]
    Text PlayerNameText, SeekerText;

    [SerializeField]
    Image imageColor;

   public Canvas playerCanvas;





    #endregion
    void Update ()
    {
        if (m_Instance.gameObject.tag == "Spectator")
        {
           
            PlayerNameText.text =  "OUT";

        }
        playerCanvas.worldCamera = PlayerCam;

    }
    void Start ()
    {
        
    }
    public void Setup()
    {

        //PlayerNameText.text = playername + m_PlayerNumber;
        
        GM = FindObjectOfType<GameManager>();
        seeker = m_Instance.GetComponent<Seeker>();
        hider = m_Instance.GetComponent<Hider>();
        spectator = m_Instance.GetComponent<Spectator>();
        playerCanvas = m_Instance.GetComponentInChildren<Canvas>();
        PlayerNameText = playerCanvas.GetComponentInChildren<Text>(gameObject.name == "PlayerName");
        
        imageColor = playerCanvas.GetComponentInChildren<Image>();
        SeekerText = imageColor.GetComponentInChildren<Text>();
        SeekerText.enabled = false;
        SeekerText.text = "SEEKER";
               
        m_Movement = m_Instance.GetComponent<RigidbodyFirstPersonController>();
        
        m_Movement.m_PlayerNumber = m_PlayerNumber;
        m_PlayerNumber = m_Movement.m_PlayerNumber;

        PlayerCam = m_Movement.GetComponentInChildren<Camera>();
       
        
        if (m_Instance.name == "Player" + m_Movement.m_PlayerNumber)
        {
            PlayerNameText.text = "Player" + m_Movement.m_PlayerNumber;
            
            
            m_Instance.gameObject.layer = LayerMask.NameToLayer("Player" + m_Movement.m_PlayerNumber); // Sets Player Layer
          
            PlayerCam.cullingMask ^= 1 << LayerMask.NameToLayer("Player" + m_Movement.m_PlayerNumber); // Sets Camera Culling Mask            

            switch (m_Movement.m_PlayerNumber)
            {
                case 1:
                    PlayerCam.rect = new Rect(0f, .52f, .49f, .48f);
                    PlayerNameText.color = Color.red;
                    imageColor.color = Color.red;
                    SeekerText.color = Color.red;

                    break;
                case 2:
                    PlayerCam.rect = new Rect(.51f, .52f, .49f, 48f);
                    PlayerNameText.color = Color.cyan;
                    imageColor.color = Color.cyan;
                    SeekerText.color = Color.cyan;
                    break;
                case 3:
                    PlayerCam.rect = new Rect(0f, 0f, .49f, .48f);
                    PlayerNameText.color = Color.yellow;
                    imageColor.color = Color.yellow;
                    SeekerText.color = Color.yellow;
                    break;
                case 4:
                    PlayerCam.rect = new Rect(.51f, 0f, .49f, .48f);
                    PlayerNameText.color = Color.green;
                    imageColor.color = Color.green;
                    SeekerText.color = Color.green;
                    break;
                default:
                    break;
            }



           

        }

        if (GM.NumOfHiders < GM.RequiredNumHiders)
        {

            if (GM.hasSeeker == true)
            {

                isHider = true;
                isSeeker = false;
                m_Instance.AddComponent<Hider>();
                AL = PlayerCam.GetComponent<AudioListener>();
                AL.enabled = false;
                GM.NumOfHiders++;
          
            }

            if (GM.hasSeeker == false)
            {

                switch (numRolled)
                {
                    case 0:
                        isHider = false;
                        isSeeker = true;
                        GM.hasSeeker = true;
                        m_Instance.AddComponent<Seeker>();
                        SeekerText.enabled = true;
                        
            

                        break;

                    case 1:
                        isHider = true;
                        isSeeker = false;
                        GM.NumOfHiders++;
                        m_Instance.AddComponent<Hider>();
                        AL = PlayerCam.GetComponent<AudioListener>();
                        AL.enabled = false;
                 

                        break;
                    case 2:
                        isHider = true;
                        isSeeker = false;
                        GM.NumOfHiders++;
                        m_Instance.AddComponent<Hider>();
                        AL = PlayerCam.GetComponent<AudioListener>();
                        AL.enabled = false;


                        break;

                    default:
                        break;
                }
            }
        }

        else if (GM.NumOfHiders == GM.RequiredNumHiders)
        {
            isHider = false;
            isSeeker = true;            
            m_Instance.AddComponent<Seeker>();
            AL = PlayerCam.GetComponent<AudioListener>();
            AL.enabled = true;
            SeekerText.enabled = true;


        }


    }


    public void RollANumber ()
    {
        numRolled = Random.Range(0, 3);

        

    }
}
