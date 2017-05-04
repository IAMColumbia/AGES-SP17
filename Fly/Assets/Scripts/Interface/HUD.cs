using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{

    // Use this for initialization
    //[SerializeField]
    //Sprite[] shieldSprites;
    //[SerializeField]
    //Image shieldUI;
    //[SerializeField]
    Sprite[] poofSprites;
    [SerializeField]
    Image poofUI;

    PlayerFlightControl player;
    private void Start()
    {
        //Make sure player gameObject has tag "Player" toggled on. 
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerFlightControl>();
    }
    private void Update()
    {
        //Visual representation of current player health.       
<<<<<<< HEAD
      // shieldUI.sprite = shieldSprites[(int)player.Shield];
=======
       //shieldUI.sprite = shieldSprites[(int)player.Shield];
>>>>>>> origin/deandre.test
       poofUI.sprite = poofSprites[(int)player.Shield];
    }
}
