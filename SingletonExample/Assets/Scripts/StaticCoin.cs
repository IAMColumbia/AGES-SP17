using UnityEngine;
using System.Collections;

public class StaticCoin : MonoBehaviour {
    float health;
    public static int CoinCount { get; set; }
    private void OnMouseDown()
    {
        StartCoroutine(WaitBeforeIncrementingCoins());
        
    }

    private IEnumerator WaitBeforeIncrementingCoins()
    {
       yield return new WaitForSeconds(0.25f);

        CoinCount++;
        Destroy(this.gameObject);
    }
}
