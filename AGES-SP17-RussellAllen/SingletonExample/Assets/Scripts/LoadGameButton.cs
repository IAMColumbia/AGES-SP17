using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadGameButton : MonoBehaviour
{
    [SerializeField]
    string sceneToLoad;

    [SerializeField]
    GameObject loadingScreen;

    void Awake()
    {
        loadingScreen.SetActive(false);
        DontDestroyOnLoad(gameObject);
    }

    public void ButtonClicked()
    {
        Debug.Log("button clicked");
        loadingScreen.SetActive(true);
        StartCoroutine(LoadNewScene());
        Debug.Log("12");
    }
    
    private IEnumerator LoadNewScene()
    {
        yield return new WaitForSeconds(2f);

        AsyncOperation async = SceneManager.LoadSceneAsync(sceneToLoad);

        while (!async.isDone)
        {
            yield return null;
        }

        yield return new WaitForSeconds(2f);

        loadingScreen.SetActive(false);
    }	
}
