using UnityEngine;
using System.Collections;

public class StaticCoin : MonoBehaviour {
    public static int CoinCount { get; set; }
    private void OnMouseDown()
    {
        StartCoroutine(WaitBeforeDestroyCoin());

    }

    private IEnumerator WaitBeforeDestroyCoin()
    {
        yield return new WaitForSeconds(1f);

        CoinCount++;
        Destroy(this.gameObject);
    }
}
