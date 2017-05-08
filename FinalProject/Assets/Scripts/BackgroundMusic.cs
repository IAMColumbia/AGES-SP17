using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    static BackgroundMusic instance = null;

	void Awake()
	{
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
	}
}