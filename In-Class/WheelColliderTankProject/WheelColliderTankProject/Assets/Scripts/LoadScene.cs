using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [SerializeField] string LevelToLoad;

    public void OnButtonClick()
    {
        SceneManager.LoadScene(LevelToLoad);
    }
}
