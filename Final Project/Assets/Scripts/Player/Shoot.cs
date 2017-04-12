using UnityEngine;
using UnityEngine.UI;

public class Shoot : MonoBehaviour
{
    public int m_PlayerNumber = 1;       
    public Rigidbody m_Shot;            
    public Transform m_FireTransform;
    //public AudioSource m_ShootingAudio;  
    //public AudioClip m_ChargingClip;     
    //public AudioClip m_FireClip;         

    private string m_FireButton;
    private float m_LaunchForce = 15f;             


    private void Start()
    {
        m_FireButton = "Fire" + m_PlayerNumber;
    }

    private void Update()
    {
		if (Input.GetButtonDown (m_FireButton))
		{
            Fire ();
			//m_ShootingAudio.Play ();
		}
    }


    private void Fire()
    {
        // Instantiate and launch the shell.

		Rigidbody shotInstance =
			Instantiate (m_Shot, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody;

        shotInstance.velocity = m_LaunchForce * m_FireTransform.forward; ;

		//m_ShootingAudio.clip = m_FireClip;
		//m_ShootingAudio.Play ();
    }
}