using UnityEngine;
using System.Collections;

public class LoadURL : MonoBehaviour {

    public void LoadURLByName(string url)
    {
        Application.OpenURL(url);
    }
}
