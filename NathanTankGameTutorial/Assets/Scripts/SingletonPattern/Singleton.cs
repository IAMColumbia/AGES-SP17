using UnityEngine;
using System.Collections;

public class SingletonPlayerSelect : MonoBehaviour
{
    public void Start()
    {
        DontDestroyOnLoad(this);
    }

    private static PlayerSelect instance;

    public static PlayerSelect Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.Find("PlayerSelect").GetComponent<PlayerSelect>();
            }

            return instance;
        }
    }
}
