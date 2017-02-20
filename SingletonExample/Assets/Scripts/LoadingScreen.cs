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
        Debug.Log("Button Clicked!!!");
        loadingScreenPanel.SetActive(true);
        StartCoroutine(LoadNewScene());
    }

    private IEnumerator LoadNewScene()
    {

        progressSlider.value = 0;

        yield return new WaitForSeconds(0.5f);


        AsyncOperation async = SceneManager.LoadSceneAsync(sceneToLoad);



        while (!async.isDone)
        {

            progressSlider.value = async.progress;

            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        progressSlider.value = 1;

        loadingScreenPanel.SetActive(false);
    }

}
