using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DisplayCoinCountOnUI : MonoBehaviour 
{
    private Text coinCountText;

    private void Start()
    {
        coinCountText = GetComponent<Text>();
        coinCountText.text = Coin.CoinCount.ToString();
    }

    private void OnEnable()
    {
        Coin.CoinCollectedEvent += CoinCollectedEventHandler;
    }

    private void CoinCollectedEventHandler()
    {
        coinCountText.text = Coin.CoinCount.ToString();
    }

    private void OnDisable()
    {
        Coin.CoinCollectedEvent -= CoinCollectedEventHandler;
    }
}
