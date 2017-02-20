using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField]
    string sceneToLoad;

    [SerializeField]
    GameObject loadingScreenPanel;

    [SerializeField]
    Text loadingText;

    bool isSceenLoaded;

    void Awake()
    {
        isSceenLoaded = false;
        loadingScreenPanel.SetActive(false);
        DontDestroyOnLoad(gameObject);
    }
        
    public void ButtonClicked()
    {
        Debug.Log("Button CLicked");
        loadingScreenPanel.SetActive(true);
        StartCoroutine(LoadNewScene());
        StartCoroutine(LoadingTextMotion());
    }

    IEnumerator LoadNewScene()
    {
        yield return new WaitForSeconds(2f);

        AsyncOperation async = SceneManager.LoadSceneAsync(sceneToLoad);

        while (!async.isDone)
        {
            yield return null;
        }

        isSceenLoaded = true;
        loadingScreenPanel.SetActive(false);
    }

    IEnumerator LoadingTextMotion()
    {
        float timeElapsed = 0;
        
        while(!isSceenLoaded)
        {
            timeElapsed += Time.deltaTime * 100;

            if (timeElapsed % 20 > 0 && timeElapsed % 20 < 5)
            {
                loadingText.text = "Loading";
            }
            else if (timeElapsed % 20 > 5 && timeElapsed % 20 < 10)
            {
                loadingText.text = "Loading.";
            }
            else if (timeElapsed % 20 > 10 && timeElapsed % 20 < 15)
            {
                loadingText.text = "Loading..";
            }
            else if (timeElapsed % 20 > 15 && timeElapsed % 20 < 20)
            {
                loadingText.text = "Loading...";
            }

            Debug.Log(timeElapsed % 20);
            yield return null;
        }
    }
}
