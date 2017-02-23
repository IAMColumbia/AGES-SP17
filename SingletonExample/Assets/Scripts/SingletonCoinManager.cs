using UnityEngine;
using System.Collections;

public class SingletonCoinManager : MonoBehaviour
{
    private static SingletonCoinManager instance;

    public static SingletonCoinManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.Find("SingletonCoinManager").GetComponent<SingletonCoinManager>();
            }

            return instance;
        }
    }

    public int CoinCount { get; set; }
}
