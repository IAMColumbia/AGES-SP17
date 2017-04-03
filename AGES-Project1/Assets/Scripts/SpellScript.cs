using UnityEngine;
using System.Collections;


public class SpellScript : MonoBehaviour {
    [SerializeField]
    AudioSource collisionSound;
    [SerializeField]
    GameObject explosion;
    public GameObject WhoOwnsThisBullet;
    public GameObject ThisBulletHitWho;

    public bool KilledAPlayer = false;
    PlayerMagic MagicScript;
	// Use this for initialization
    void Awake()
    {
        collisionSound = GetComponent<AudioSource>();
        /*
        Manager = GameObject.FindObjectOfType<GameManager>();
        for (int i = 0; i < Manager.Players.Length; i++)
        {
            WhoOwnsThisBullet = Manager.Players[i].Instance.name;
        }
        */
        collisionSound.Play();
    }
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        
    }
    void OnCollisionEnter(Collision other)
    {
        Instantiate(explosion, gameObject.transform.position, gameObject.transform.rotation);
        if (other.gameObject.tag == "Player")
        {
            
            Debug.Log("Collision.");
            ThisBulletHitWho = other.gameObject;
            MagicScript = WhoOwnsThisBullet.GetComponent<PlayerMagic>();
            MagicScript.WhoDidThisPlayerLastHit = other.gameObject;
            if (ThisBulletHitWho.GetComponent<PlayerMagic>().playerHealth == 10)
            {
                WhoOwnsThisBullet.GetComponent<PlayerMagic>().KillCount++;

            }
        }
        
        Destroy(gameObject);
        
    }
    
    
}
