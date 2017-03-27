using UnityEngine;
using System.Collections;

public class LoadHyperLinks : MonoBehaviour {

    [SerializeField]
    string hyperLink;

    public void LoadHyperLinkByName()
    {
        Application.OpenURL(hyperLink);
    }
}
