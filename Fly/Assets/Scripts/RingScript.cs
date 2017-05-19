using UnityEngine;
using System.Collections;

public class RingScript : MonoBehaviour {

    // Use this for initialization
 
    [SerializeField]
    GameObject ring;

    float rotationSpeed = 75f;
    
 
    bool istriggered;

    GameManager gameManager;

    GameObject m_RoundWinner;

    GameObject m_GameWinner;


    private void Start()
    {
      //  m_RoundWinner = gameManager.m_RoundWinner;
    }
    private void Update()
    {
       
        if (!ring.activeSelf && istriggered == false)
        {
         //   m_RoundWinner = GameObject.FindGameObjectWithTag("RoundWinner");
         
            //if (HasGoal)
            //{
            //    ring.SetActive(true);
            //}
        }
       
        //if (!HasGoal)
        //{
        //    istriggered = true;
        //    transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        //}                       
    }

    void OnTriggerEnter(Collider other)
    {

        if (istriggered == false)
        {
            if (other.gameObject.tag == "Player")
            {            
               istriggered = true;                
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        istriggered = false;
       
        ring.SetActive(false);
    }
    // do your things here that has to happen once
}

