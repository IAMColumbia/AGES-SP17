using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.ThirdPerson;

    public class TankShooting : MonoBehaviour
    {
        public int m_PlayerNumber = 1;
        public GameObject bullet;
        public GameObject bulletEmitter;

        public AudioSource m_ShootingAudio;  
        public AudioClip m_ChargingClip;     
        public AudioClip m_FireClip;

    [SerializeField]
    float bulletForwardForce;
    private string m_FireButton;

        private void Start()
        {
            m_FireButton = "Fire" + m_PlayerNumber;     
        }
        private void Update()
        {
            // Track the current state of the fire button and make decisions based on the current launch force.
                   
         if (Input.GetButton(m_FireButton))
            {
            //Debug.Log("Player Fired Weapon");
          
            Fire(); 
               
                m_ShootingAudio.clip = m_ChargingClip;
                m_ShootingAudio.Play();
            }              
        }

        private void Fire()
        {
        // Instantiate and launch the shell.

        GameObject temporaryBulletHandler;
        temporaryBulletHandler = Instantiate(bullet, bulletEmitter.transform.position, bulletEmitter.transform.rotation) as GameObject;

                Rigidbody Temporary_RigidBody;
                Temporary_RigidBody = temporaryBulletHandler.GetComponent<Rigidbody>();

        //Tell the bullet to be "pushed" forward by an amount set by Bullet_Forward_Force.
                  Temporary_RigidBody.AddForce(transform.forward * bulletForwardForce * Time.deltaTime);

        //Basic Clean Up, set the Bullets to self destruct after 10 Seconds, I am being VERY generous here, normally 3 seconds is plenty.     
            m_ShootingAudio.clip = m_FireClip;
            m_ShootingAudio.Play();
          
            Destroy(temporaryBulletHandler, .5f);       
    }
}
