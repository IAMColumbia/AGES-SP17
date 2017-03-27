using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    //Management
    [HideInInspector] public float playerNumber;

    [HideInInspector] public bool isAlive;


    //Movement
    float movementSpeed;

    [HideInInspector] public GameObject target;

    Rigidbody rigidbody;


    //Boosting
    bool isReadyToBoostRight;

    bool isReadyToBoostLeft;

    float boostCooldown;

    float boostPower;

    float boostDuration;

    bool isBoosting;

    [SerializeField]
    ParticleSystem boostTrail;
    ParticleSystem.EmissionModule boostTrailEmission;

    float boostTrailOn;
    float boosttrailOff;


    //shooting
    float bulletSpread;

    float maxBulletSpread;

    float bulletSpeed;

    [SerializeField]
    Rigidbody projectile;

    [SerializeField]
    GameObject firingPoint;

    [SerializeField]
    GameObject gun;

    int remainingAmmo;

    int maxAmmo;

    float reloadTimer;

    bool isReloading;


    //Shielding
    [SerializeField]
    GameObject shield;

    [SerializeField]
    GameObject deployingPoint;

    GameObject shieldInstance;
    bool isReadyToDeployShield;
    bool isShielding;
    float shieldDuration;
    float shieldCooldown;


    //Audio

    [HideInInspector]public AudioSource audiosource;

    [SerializeField]
    public AudioClip[] clip;


    //HUD
    [SerializeField]
    public Image[] imagesHUD;
    

    // Use this for initialization
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        audiosource = GetComponent<AudioSource>();

        bulletSpread = 0;
        maxBulletSpread = 10;
        bulletSpeed = 3000;
        movementSpeed = 500;
        rigidbody.mass = 1;
        isAlive = true;
        maxAmmo = 200;
        remainingAmmo = maxAmmo;
        reloadTimer = 5f;
    
        isReadyToBoostRight = true;
        isReadyToBoostLeft = true;
        isBoosting = false;
        boostCooldown = 3f;
        boostPower = 500f;
        boostDuration = 2f;
        boostTrailEmission = boostTrail.emission;
        boostTrailOn = 10f;
        boosttrailOff = 0f;
        boostTrailEmission.rate = boosttrailOff;

        isReadyToDeployShield = true;
        isShielding = false;
        shieldDuration = 7f;
        shieldCooldown = 10f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        gameObject.tag = "P" + playerNumber;

        if (gameObject.tag == "P1")
        {
            target = GameObject.FindGameObjectWithTag("P2");
        }

        else
        {
            target = GameObject.FindGameObjectWithTag("P1");
        }

        if (isAlive == true)
        {
            BasicMovementInput();
            BodyRotation();
            ShootingControls();
            GunRotation();
            Boosters();
            ShieldingControls();
            UpdateHUD();
        }

        AxisControl();
    }

    public IEnumerator DeactivateMecha()
    {
        yield return new WaitForSeconds(audiosource.clip.length);

        rigidbody.gameObject.SetActive(false);
    }

    public void ExplosionSound()
    {
        audiosource.clip = clip[2];
        audiosource.volume = 1f;
        audiosource.pitch = 1f;
        audiosource.Play();
    }

    void AxisControl()
    {
        if (isAlive == true)
        {
            rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionY;
        }

        else
        {
            rigidbody.constraints = RigidbodyConstraints.FreezeAll;    
        }
    }

    private void BodyRotation()
    {
        if (target != null)
        {
            Vector3 targetDir = target.transform.position - transform.position;
            float turnSpeed = movementSpeed * Time.deltaTime;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, turnSpeed, 0.0F);
            transform.rotation = Quaternion.LookRotation(newDir);
        }
        
    }

    void BasicMovementInput()
    {
        float zMovement = Input.GetAxis("P" + playerNumber + "_Vertical") * movementSpeed * Time.deltaTime;

        float xMovement = Input.GetAxis("P" + playerNumber + "_Horizontal") * movementSpeed * Time.deltaTime;

        Vector3 movementVector = new Vector3(xMovement, 2.5f, zMovement);

        rigidbody.AddRelativeForce(movementVector * movementSpeed * Time.deltaTime);
    }

    void Boosters()
    {
        if (Input.GetButtonDown("P" + playerNumber + "_RightBoost") && isReadyToBoostRight && !isBoosting)
        {
            StartCoroutine(RightBoost());
        }

        if (Input.GetButtonDown("P" + playerNumber + "_LeftBoost") && isReadyToBoostLeft && !isBoosting)
        {
            StartCoroutine(LeftBoost());
        }
    }

    IEnumerator RightBoost()
    {
        isReadyToBoostRight = false;
        isBoosting = true;
        boostTrailEmission.rate = boostTrailOn;

        float rightSideBoostStrength = ((Mathf.Abs(boostPower)) * -1);
        Vector3 boostVectorRight = new Vector3(rightSideBoostStrength, 0, 0);

        while (isBoosting == true)
        { 
            rigidbody.AddRelativeForce(boostVectorRight * movementSpeed * Time.deltaTime);

            yield return new WaitForSeconds(boostDuration);

            boostTrailEmission.rate = boosttrailOff;
            isBoosting = false;
        } 

        yield return new WaitForSeconds(boostCooldown);
        isReadyToBoostRight = true;
    }

    IEnumerator LeftBoost()
    {
        isReadyToBoostLeft = false;
        isBoosting = true;
        boostTrailEmission.rate = boostTrailOn;

        float leftSideBoostStrength = ((Mathf.Abs(boostPower)));
        Vector3 boostVectorLeft = new Vector3(leftSideBoostStrength, 0, 0);

        while (isBoosting == true)
        {
            rigidbody.AddRelativeForce(boostVectorLeft * movementSpeed * Time.deltaTime);

            yield return new WaitForSeconds(boostDuration);

            boostTrailEmission.rate = boosttrailOff;
            isBoosting = false;
        }

        yield return new WaitForSeconds(boostCooldown);
        isReadyToBoostLeft = true;
    }

    private void ShieldingControls()
    {
        if (Input.GetAxis("P" + playerNumber + "_Alt") > 0 && isReadyToDeployShield)
        {
            audiosource.clip = clip[3];
            audiosource.volume = 0.6f;
            audiosource.Play();
            StartCoroutine(ShieldBehavior());
        }
    }

    IEnumerator ShieldBehavior()
    {
        isReadyToDeployShield = false;
        isShielding = true;

        shieldInstance = (GameObject)Instantiate(shield, deployingPoint.transform.position, deployingPoint.transform.rotation);
        ShieldController instanceController = shieldInstance.GetComponent<ShieldController>(); 

        while (isShielding == true)
        {
            yield return new WaitForSeconds(shieldDuration);

            instanceController.BeginRelease(target);

            if (instanceController.isFinishedReleasing)
            {
                Destroy(shieldInstance);

                isShielding = false;
            }
        }

        yield return new WaitForSeconds(shieldCooldown);

        isReadyToDeployShield = true;
    }

    private void ShootingControls()
    {
        if (Input.GetButtonDown("P" + playerNumber + "_Reload") && isReloading == false)
        {
            StartCoroutine(Reload());
        }

        Vector3 bulletSpreadPattern = new Vector3((transform.rotation.x + (Random.Range(-1f, 1f) * bulletSpread)),
                    (transform.rotation.y + (Random.Range(-1f, 1f) * bulletSpread)), 0);

        if (Input.GetAxis("P" + playerNumber + "_Fire") > 0 && isReloading == false)
        {
            if (remainingAmmo <= 0)
            {
                StartCoroutine(Reload());
            }

            Rigidbody clone = (Rigidbody)Instantiate(projectile, firingPoint.transform.position, firingPoint.transform.rotation);
            clone.transform.Rotate(bulletSpreadPattern);
            clone.AddForce(clone.transform.forward * bulletSpeed);

            remainingAmmo--;

            audiosource.clip = clip[0];
            audiosource.volume = 1f;
            audiosource.pitch = Random.Range(2.5f, 3f);
            audiosource.Play();

            if (bulletSpread < maxBulletSpread)
            {
                bulletSpread = bulletSpread + .05f;
            }
        }

        else
        {
            if (bulletSpread > 0)
            {
                bulletSpread = bulletSpread - .1f;
            }
        }
    }

    private void GunRotation()
    {
        if (target != null)
        {
            Vector3 targetDir = target.transform.position - gun.transform.position;
            float turnSpeed = movementSpeed * Time.deltaTime;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, turnSpeed, 0.0F);
            gun.transform.rotation = Quaternion.LookRotation(newDir);
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;

        yield return new WaitForSeconds(reloadTimer);

        remainingAmmo = maxAmmo;

        isReloading = false;
    }

    public void ResetBoosters()
    {
        isReadyToBoostRight = true;
        isReadyToBoostLeft = true;
        isBoosting = false;
    }
        
    public void ResetShields()
    {
        isReadyToDeployShield = true;
        isShielding = false;
        if (shieldInstance != null)
        {
            Destroy(shieldInstance);
        }
        
    }

    void UpdateHUD()
    {
        if (Input.GetButton("P" + playerNumber + "_HUD"))
        {
            for (int i = 0; i < imagesHUD.Length; i++)
            {
                imagesHUD[i].enabled = true;
            }

            if (isReloading)
            {
                imagesHUD[0].GetComponent<Image>().color = new Color32(255, 0, 0, 100);
            }
            if (!isReloading)
            {
                imagesHUD[0].GetComponent<Image>().color = new Color32(0, 255, 0, 100);
            }

            if (!isReadyToDeployShield)
            {
                imagesHUD[1].GetComponent<Image>().color = new Color32(255, 0, 0, 100);
            }
            if (isReadyToDeployShield)
            {
                imagesHUD[1].GetComponent<Image>().color = new Color32(0, 255, 0, 100);
            }

            if (!isReadyToBoostRight)
            {
                imagesHUD[2].GetComponent<Image>().color = new Color32(255, 0, 0, 100);
            }
            if (isReadyToBoostRight && !isBoosting)
            {
                imagesHUD[2].GetComponent<Image>().color = new Color32(0, 255, 0, 100);
            }

            if (!isReadyToBoostLeft)
            {
                imagesHUD[3].GetComponent<Image>().color = new Color32(255, 0, 0, 100);
            }
            if (isReadyToBoostLeft && !isBoosting)
            {
                imagesHUD[3].GetComponent<Image>().color = new Color32(0, 255, 0, 100);
            }
        }

        else
        {
            for (int i = 0; i < imagesHUD.Length; i++)
            {
                imagesHUD[i].enabled = false;
            }
        }
    }
        
}
