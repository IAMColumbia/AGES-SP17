using UnityEngine;
using System.Collections;

public delegate void CoinCollectedDelegate();

public class Coin : MonoBehaviour 
{
    public static event CoinCollectedDelegate CoinCollectedEvent;
    public static int CoinCount { get; set; }

    private void OnMouseDown()
    {
        CoinCount++;

        if (CoinCollectedEvent != null)
            CoinCollectedEvent.Invoke();

        Destroy(gameObject);
    }

}
