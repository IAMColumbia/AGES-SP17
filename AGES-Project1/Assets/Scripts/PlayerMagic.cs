using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerMagic : MonoBehaviour , IDamageable<float>{
    [SerializeField]
    GameObject projectile;

    [SerializeField]
    Transform magicSpawn;

    [SerializeField]
    Transform shieldSpawn;

    [SerializeField]
    float thrust = 100;

    [SerializeField]
    string fireButton = "Fire1_P1";

    [SerializeField]
    string fireButtonTwo = "Fire2_P1";

    [SerializeField]
    string fireButtonThree = "Fire3_P1";

    [SerializeField]
    GameObject sheildWall;

    [SerializeField]
    GameObject projectableBlockWall;

    [SerializeField]
    AudioSource shieldConjure;
    public float playerHealth;
    
    public bool alive;
    public GameObject WhoDidThisPlayerLastHit = null;
    public GameObject WhoHitThisPlayerLast = null;
   
    public int PlayerNumber = 1;
    string thisPlayersSpell;
    Text score;
    Slider healthSlider;
    Image fillImage;
    bool canLaunchIce = false;
    bool projectingIce = false;
    GameObject clonedProjectile;
    GameObject clonedSheildWall;

    SpellScript PlayerSpell;

    public int KillCount;
    // Use this for initialization
    void Start ()
    {
        //shieldConjure = GetComponent<AudioSource>();
        score = gameObject.GetComponentInChildren<Text>();
        healthSlider = gameObject.GetComponentInChildren<Slider>();
        fillImage = gameObject.GetComponentInChildren<Image>();
        SetInputs();
        gameObject.name = "Player" + PlayerNumber;
    }
	
	// Update is called once per frame
	void Update ()
    {
        CastSpell();
        CheckIfPlayerIsAlive();
        score.text = KillCount.ToString();
        
    }

    void CastSpell()
    {
        MagicBolt();
        CastSheild();
        ProjectBlocks();
       
    }

    void CastSheild()
    {
        if (Input.GetButtonDown(fireButtonTwo))
        {
            clonedSheildWall = Instantiate(sheildWall, shieldSpawn.position, shieldSpawn.rotation) as GameObject;
            shieldConjure.Play();
        }
    }

    void MagicBolt()
    {
        if (Input.GetButtonDown(fireButton))
        {
            clonedProjectile = Instantiate(projectile, magicSpawn.position, Quaternion.identity) as GameObject;
            PlayerSpell = FindObjectOfType<SpellScript>();
            PlayerSpell.WhoOwnsThisBullet = gameObject;
            
            clonedProjectile.tag = "EnemySpell";
            clonedProjectile.GetComponent<Rigidbody>().AddForce(magicSpawn.forward * thrust);

        }
    }

    void ProjectBlocks()
    {
        if(Input.GetButtonDown(fireButtonThree))
        {
            projectingIce = true;
        }
    }

   /* void OnTriggerEnter(Collider other)
    {
        canLaunchIce = true;
        Debug.Log("Can Launch Ice.");
        if(other.tag == clonedSheildWall.tag && projectingIce)
        {
            Debug.Log("Projecting Blocks!");
            Instantiate(projectableBlockWall, clonedSheildWall.transform.position, clonedSheildWall.transform.rotation);
            Destroy(clonedSheildWall);
            canLaunchIce = false;
        }
    }*/
    void OnTriggerExit(Collider other)
    {
        canLaunchIce = false;
        Debug.Log("Cannot Launch Ice.");
    }
    void SetInputs()
    {
        fireButton = "Fire1_P" + PlayerNumber.ToString();
        fireButtonTwo = "Fire2_P" + PlayerNumber.ToString();
        fireButtonThree = "Fire3_P" + PlayerNumber.ToString();
    }
    public void TakeDamage(float damageTaken)
    {
        damageTaken = 10;
        playerHealth = playerHealth - damageTaken;
        SetHealthUI();
    }

    void OnCollisionEnter(Collision other)
    {
        
        if (other.gameObject.tag == "EnemySpell")
        {


            WhoHitThisPlayerLast = other.gameObject.GetComponent<SpellScript>().WhoOwnsThisBullet;

            Debug.Log(WhoHitThisPlayerLast.GetComponent<PlayerMagic>().name);

            
            TakeDamage(playerHealth);
            Debug.Log("Player " + PlayerNumber + ":" + playerHealth);
            //AdjustScore();
            //Destroy(other.gameObject);
        }
        
    }
    void CheckIfPlayerIsAlive()
    {
        if (playerHealth <= 0)
        {

            alive = false;
            
        }
        
    }
     void SetHealthUI()
    {
        healthSlider.value = playerHealth;

    }
}
