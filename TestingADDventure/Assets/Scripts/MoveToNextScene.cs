using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MoveToNextScene : MonoBehaviour
{
    [SerializeField]
    string nextScene;

    public void LoadNextScene()
    {
        SceneManager.LoadScene(nextScene);
    }
}
