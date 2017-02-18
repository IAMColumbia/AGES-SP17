using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField]
    GameObject loadingScreenBackground;

    [SerializeField]
    Slider progressSlider;

    private static LoadingScreen instance;

    public static LoadingScreen Instance
    {
        get
        {
            if (instance == null)
            {
                try
                {
                    instance = GameObject.Find("Loading Screen Canvas").GetComponent<LoadingScreen>();
                }
                catch (Exception)
                {
                    throw new System.Exception("Scene must contain the loading scene prefab named 'Loading Screen Canvas'"); ;
                }
                  
                DontDestroyOnLoad(instance);
            }

            return instance;
        }
    }

    void Awake()
    {
        loadingScreenBackground.SetActive(false);
    }

    public void LoadScene(string sceneName)
    {
        progressSlider.value = 0;

        loadingScreenBackground.SetActive(true);

        StartCoroutine(LoadNewScene(sceneName));
    }

    IEnumerator LoadNewScene(string sceneName)
    {
        // The scene loads so quickly that we insert all these
        // WaitForSeconds to smooth out the animation and make it
        // easier to see.

        yield return new WaitForSeconds(0.2f);

        AsyncOperation loading = SceneManager.LoadSceneAsync(sceneName);
        progressSlider.value = loading.progress;
        Debug.Log(String.Format("Loading progress: {0}", loading.progress));

        while (!loading.isDone)
        {
            Debug.Log(String.Format("Loading progress: {0}", loading.progress));
            progressSlider.value = loading.progress;
            yield return new WaitForSeconds(0.2f);
        }

        progressSlider.value = loading.progress;

        yield return new WaitForSeconds(0.2f);

        loadingScreenBackground.SetActive(false);
    }
}
