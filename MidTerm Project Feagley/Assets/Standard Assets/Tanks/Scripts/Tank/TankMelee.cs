using UnityEngine;
using System.Collections;

public class TankMelee : MonoBehaviour
{
    //variables
    private string m_AttackButton;
    private float m_AttackPower;

    public Rigidbody m_Sword;



	// Use this for initialization
	void Start ()
    {
        Destroy(m_Sword);
        Rigidbody swordInstance = Instantiate(m_Sword);	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void Swing()
    {

    }
}
