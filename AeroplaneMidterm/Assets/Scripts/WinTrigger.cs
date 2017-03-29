using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinTrigger : MonoBehaviour
{

    bool playerOneHasWon = false;
    bool playerTwoHasWon = false;
    bool gameover = false;

    public GameObject CheaterTrigger;
    public GameObject Player1Text;
    public GameObject Player2Text;
    public GameObject AText;

    // Use this for initialization
    void Start()
    {
        MeshRenderer meshRender = this.gameObject.GetComponent<MeshRenderer>();
        //Make self invisible
        meshRender.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (CheaterTrigger.GetComponent<CheaterTrigger>().playerOneHasPassed == true)
        {
            playerOneHasWon = true;
        }

        if (CheaterTrigger.GetComponent<CheaterTrigger>().playerTwoHasPassed == true)
        {
            playerTwoHasWon = true;
        }

        LevelSwap();
    }

    void OnTriggerEnter(Collider other)
    {
        //if (other.tag == "Player")
        //{
        //    if (CheaterTrigger.GetComponent<CheaterTrigger>().playerOneHasPassed == true)
        //    {
        //        //playerOneHasWon = true;
        //        CanvasText.enabled = true;
        //        Time.timeScale = 0;
        //    }
        //}
        //if (other.tag == "Player2")
        //{
        //    if (CheaterTrigger.GetComponent<CheaterTrigger>().playerTwoHasPassed == true)
        //    {
        //       //playerTwoHasWon = true;
        //    }
        //}

        if (other.tag == "Player")
        {
            if (playerOneHasWon)
            {
                Player1Text.SetActive(true);
                Time.timeScale = 0.2f;
                gameover = true;
            }
            else
            {
                return;
            }
        }
        if (other.tag == "Player2")
        {
            if (playerTwoHasWon)
            {
                Player2Text.SetActive(true);
                Time.timeScale = 0.2f;
                gameover = true;
            }
            else
            {
                return;
            }
        }

    }

    void LevelSwap()
    {
        if (gameover && Input.GetKeyDown("joystick 1 button 0"))
        {
            Debug.Log("nowLoading");
            SceneManager.LoadScene("MainMenu");
        }
    }
}
