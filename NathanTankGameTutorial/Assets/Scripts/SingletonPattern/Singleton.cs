using UnityEngine;
using System.Collections;

public class SingletonPlayerSelect : MonoBehaviour
{
    private static PlayerSelect instance;

    public static PlayerSelect Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.Find("GameManager").GetComponent<PlayerSelect>();
            }

            return instance;
        }
    }
}
