using UnityEngine;
using System.Collections;

public class StaticCoin : MonoBehaviour
{
    public static int CoinCount { get; set; }
    private void OnMouseDown()
    {
        StartCoroutine(WaitBeforeIncrementingCoins());
    }

    private IEnumerator WaitBeforeIncrementingCoins()
    {
        yield return new WaitForSeconds(2);
        CoinCount++;
        Destroy(this.gameObject);
    }
}
