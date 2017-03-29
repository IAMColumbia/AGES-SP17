using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class TitleMenu : MonoBehaviour {

    [SerializeField]
    string sceneToLoad;

    public void ClickedBeginButton()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
