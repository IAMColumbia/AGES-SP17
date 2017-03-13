using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StaticCoinCountLabel : MonoBehaviour {

    private Text labelText;

    private void Start()
    {
        labelText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        labelText.text = StaticCoin.CoinCount.ToString();
    }
}
