using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameStart : MonoBehaviour
{
    List<TankMovement> tanks = new List<TankMovement>(); //used to determine how many tanks are inside this collider
    public string LevelToLoad;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<TankMovement>() != null  //if the other collider has a tankMovement script
           && !tanks.Contains(other.gameObject.GetComponent<TankMovement>())) //AND the list does not already contain this player
        {
            tanks.Add(other.gameObject.GetComponent<TankMovement>()); //the "other" must be a tank!  Add it to the list
        }

        //Debug.Log("There are " + tanks.Count + " tank(s) inside this trigger");
        if (tanks.Count >= SingletonPlayerSelect.Instance.CurrentPlayers.Count //if all the players are in the start trigger
            && SingletonPlayerSelect.Instance.CurrentPlayers.Count > 1) //AND if there is more than one player 
        {
            StartGame();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.GetComponent<TankMovement>() != null) //if the other collider has a tankMovement script
        {
            tanks.Remove(other.gameObject.GetComponent<TankMovement>()); //the "other" must be a tank!  Remove it from the list
        }
    }

    void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(LevelToLoad);
    }
}
