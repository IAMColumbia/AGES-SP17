using UnityEngine;
using System.Collections;

public class StaticCoin : MonoBehaviour {
    public static int CoinCount { get; set; }
    private void OnMouseDown()
    {
        StartCoroutine(WaitBeforeIncrementingCoins());
        CoinCount++;
        Destroy(this.gameObject);
    }

    private IEnumerator WaitBeforeIncrementingCoins()
    {
        yield return new WaitForSeconds(5);
        CoinCount++;
        Destroy(this.gameObject);
    }
}

