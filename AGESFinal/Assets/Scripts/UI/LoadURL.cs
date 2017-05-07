using UnityEngine;
using System.Collections;

public class LoadURL : MonoBehaviour {

	public void LoadURLLink(string link)
    {
        Application.OpenURL(link);
    }
}
