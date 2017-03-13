using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] string sceneToLoad;
    [SerializeField] GameObject loadingScreenPanel;
    [SerializeField] Slider progressSlider;

    void Awake()
    {
        loadingScreenPanel.SetActive(false);
        DontDestroyOnLoad(gameObject);
    }

    public void ButtonClicked()
    {
        Debug.Log("Wake up Grab a brush and put a little (makeup)  Hide the scars to fade away the (shakeup) Hide the scars to fade away the Why'd you leave the keys upon the table? Here you go create another fable You wanted to Grab a brush and put a little makeup You wanted to Hide the scars to fade away the shakeup You wanted to Why'd you leave the keys upon the table? You wanted to I don't think you trust In, my, self righteous suicide I, cry, when angels deserve to die, DIE Wake up Grab a brush and put a little (makeup) Grab a brush and put a little Hide the scars to fade away the (shakeup) Hide the scars to fade away the Why'd you leave the keys upon the table? Here you go create another fable You wanted to Grab a brush and put a little makeup You wanted to Hide the scars to fade away the shakeup You wanted to Why'd you leave the keys upon the table? You wanted to I don't think you trust In, my, self righteous suicide I, cry, when angels deserve to die In, my, self righteous suicide I, cry, when angels deserve to die Father, father, father, father Father into your hands, I commend my spirit Father into your hands why have you forsaken me In your eyes forsaken me In your thoughts forsaken me In your heart forsaken, me oh Trust in my self righteous suicide I, cry, when angels deserve to die In my self righteous suicide I, cry, when angels deserve to die");
        loadingScreenPanel.SetActive(true);
        StartCoroutine(LoadNewScene());
    }

    private IEnumerator LoadNewScene()
    {
        progressSlider.value = 0;

        yield return new WaitForSeconds(.5f);

        AsyncOperation async = SceneManager.LoadSceneAsync(sceneToLoad);

        while(!async.isDone)    //While level is still loading
        {
            progressSlider.value = async.progress;  //Sets slider to current level load progress

            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        progressSlider.value = async.progress;
        loadingScreenPanel.SetActive(false);
    }
}
