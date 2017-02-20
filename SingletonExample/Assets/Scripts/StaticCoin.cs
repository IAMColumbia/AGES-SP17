using UnityEngine;
using System.Collections;

public class StaticCoin : MonoBehaviour {
    public static int CoinCount { get; set; }
    private void OnMouseDown()
    {
        CoinCount++;
        Destroy(this.gameObject);
    }
}
