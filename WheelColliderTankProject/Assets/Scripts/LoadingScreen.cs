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
        string loading = "Loading";
        string period = ".";
        
        while(!isSceenLoaded)
        {
            timeElapsed += Time.deltaTime * 100;

            if (timeElapsed % 35 > 0 && timeElapsed % 35 < 5)
            {
                loadingText.text = loading;
            }
            else if (timeElapsed % 35 > 5 && timeElapsed % 35 < 10)
            {
                loadingText.text += period;
            }
            else if (timeElapsed % 35 > 10 && timeElapsed % 35 < 15)
            {
                loadingText.text += period;
            }
            else if (timeElapsed % 35 > 15 && timeElapsed % 35 < 20)
            {
                loadingText.text += period;
            }
            else if (timeElapsed % 35 > 20 && timeElapsed % 35 < 25)
            {
                loadingText.text += period;
            }
            //else if (timeElapsed % 35 > 25 && timeElapsed % 35 < 30)
            //{
            //    loadingText.text += period;
            //}

            Debug.Log(timeElapsed % 35);
            yield return null;
        }
    }
}
