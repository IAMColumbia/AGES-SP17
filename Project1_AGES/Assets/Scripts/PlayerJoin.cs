using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerJoin : MonoBehaviour {

    //[SerializeField]
    //private List<Transform> players;
    //public bool gameStart;

    [SerializeField]
    private GameObject roundManager;
    [SerializeField]
    private GameObject scoreBoard;
    
    [SerializeField]
    private GameObject p1;
    [SerializeField]
    private GameObject p2;
    [SerializeField]
    private GameObject p3;
    [SerializeField]
    private GameObject p4;

    [SerializeField]
    private GameObject p1Text1;
    [SerializeField]
    private GameObject p1Text2;

    [SerializeField]
    private GameObject p2Text1;
    [SerializeField]
    private GameObject p2Text2;

    [SerializeField]
    private GameObject p3Text1;
    [SerializeField]
    private GameObject p3Text2;

    [SerializeField]
    private GameObject p4Text1;
    [SerializeField]
    private GameObject p4Text2;

    private int numberOfPlayersJoined;


    // Use this for initialization
    void Start () {

        roundManager.SetActive(false);

        //for (int i = 1; i <= players.Count; i++)
        //{

        //}
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButtonDown("Enter1"))
        {
            p1.SetActive(true);
            p1Text1.SetActive(false);
            p1Text2.SetActive(true);
            numberOfPlayersJoined++;
        }

        if (Input.GetButtonDown("Enter2"))
        {
            p2.SetActive(true);
            p2Text1.SetActive(false);
            p2Text2.SetActive(true);
            numberOfPlayersJoined++;
        }

        if (Input.GetButtonDown("Enter3"))
        {
            p3.SetActive(true);
            p3Text1.SetActive(false);
            p3Text2.SetActive(true);
            numberOfPlayersJoined++;
        }

        if (Input.GetButtonDown("Enter4"))
        {
            p4.SetActive(true);
            p4Text1.SetActive(false);
            p4Text2.SetActive(true);
            numberOfPlayersJoined++;
        }

        if(Input.GetButtonDown("Start") && numberOfPlayersJoined >= 2)
        {
            roundManager.SetActive(true);
            scoreBoard.SetActive(true);
            gameObject.SetActive(false);
        }

    }
}
