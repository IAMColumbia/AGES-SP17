using UnityEngine;
using System.Collections;

public class SingletonCoin : MonoBehaviour
{
    private void OnMouseDown()
    {
        SingletonCoinManager.Instance.CoinCount++;
        Destroy(this.gameObject);
    }
}
