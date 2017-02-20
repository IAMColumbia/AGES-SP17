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
    Slider progressSlider;

    void Awake()
    {
        loadingScreenPanel.SetActive(false);
        DontDestroyOnLoad(gameObject);
    }

    public void ButtonClicked()
    {
        Debug.Log("button clicked");
        loadingScreenPanel.SetActive(true);
        StartCoroutine(LoadNewScene());
        Debug.Log("12");
    }
    
    private IEnumerator LoadNewScene()
    {
        progressSlider.value = 0;

        yield return new WaitForSeconds(0.1f);

        AsyncOperation async = SceneManager.LoadSceneAsync(sceneToLoad);
        
        while (!async.isDone)
        {
            progressSlider.value = async.progress;

            yield return null;
                        
        }

        yield return new WaitForSeconds(2f);

        progressSlider.value = async.progress;

        loadingScreenPanel.SetActive(false);
    }	
}
