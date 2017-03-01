using UnityEngine;
using System.Collections;
using System;
public class GameManager : MonoBehaviour 
{
    [SerializeField]
    private int coinsRequiredToWin = 3;

    [SerializeField]
    private string message = "You collected all the coins!";

    public static event Action<string> PlayerWonEvent;

    private void CoinCollectedEventHandler()
    {
        if (Coin.CoinCount == coinsRequiredToWin)
        {
            if (PlayerWonEvent != null)
                PlayerWonEvent.Invoke(message);
        }
    } 

    private void OnEnable()
    {
        Coin.CoinCollectedEvent += CoinCollectedEventHandler;
    }
    private void OnDisable()
    {
        Coin.CoinCollectedEvent -= CoinCollectedEventHandler;
    }

}
