using UnityEngine;
using System.Collections;

public class MenuMusic : MonoBehaviour
{
    static MenuMusic instance = null;

    public static MenuMusic Instance
    {
        get
        {
            return instance;
        }
    }

	void Awake()
	{
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
	}

    public void StopMusic()
    {
        Destroy(this.gameObject);
    }
}