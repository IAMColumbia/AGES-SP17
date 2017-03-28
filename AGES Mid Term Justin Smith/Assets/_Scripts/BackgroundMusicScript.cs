using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class BackgroundMusicScript : MonoBehaviour
{
    [SerializeField]
    AudioClip[] menuBGMusicSelection;
    [SerializeField]
    AudioClip roundStartBGMusic;
    [SerializeField]
    AudioClip roundLastPillarMusic;
    [SerializeField]
    GameObject backgroundMusicContainer;
    [SerializeField]
    AudioSource audioSource;

    Scene activeScene;
    float secondsBeforeLastPillarStanding = 100;
    void Start()
    {
        DontDestroyOnLoad(backgroundMusicContainer);
        PlayRandomMenuMusic();
    }

    IEnumerator WaitToChangeBGMusic()
    {
        yield return new WaitForSeconds(secondsBeforeLastPillarStanding);
        audioSource.clip = roundLastPillarMusic;
        audioSource.Play();
    }

    void PlayRandomMenuMusic()
    {
        audioSource.clip = menuBGMusicSelection[Random.Range(0, menuBGMusicSelection.Length)];
        audioSource.Play();
    }

    void OnEnable()
    {
        //Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnDisable()
    {
        //Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as this script is disabled. Remember to always have an unsubscription for every delegate you subscribe to!
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        //Debug.Log("Level Loaded");
        //Debug.Log(scene.name);
        //Debug.Log(mode);
        if (scene.buildIndex == 1)
            audioSource.clip = roundStartBGMusic;
        audioSource.Play();

        StartCoroutine(WaitToChangeBGMusic());
    }



}
