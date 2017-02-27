using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadGameButton : MonoBehaviour
{
    [SerializeField] string sceneToLoad;
    [SerializeField] GameObject loadingScreen;

    void Awake()
    {
        loadingScreen.SetActive(false);
        DontDestroyOnLoad(loadingScreen);
    }
    public void ButtonClicked()
    {
        loadingScreen.SetActive(true);
        StartCoroutine(LoadNewScene());
    }
	
    private IEnumerator LoadNewScene()
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneToLoad);

        while (!async.isDone)
        {
            yield return null;
        }

        yield return new WaitForSeconds(2);
        loadingScreen.SetActive(false);
        //progress bar
    }

}
