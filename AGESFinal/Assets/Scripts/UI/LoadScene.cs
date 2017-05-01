using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {

    public void LoadPlayerAmountScene(string level)
    {
        SceneManager.LoadScene(level);
    }
}
