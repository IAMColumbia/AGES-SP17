using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public static class LoadingSceneManager 
{
    public const string loadingSceneName = "loading scene";

    private static string sceneToLoad = "test with terrain";

    public static string SceneToLoad
    {
       get { return sceneToLoad; }
    }

    public static void LoadNewScene(string sceneName)
    {
        sceneToLoad = sceneName;
        SceneManager.LoadScene(loadingSceneName);
    }



}
