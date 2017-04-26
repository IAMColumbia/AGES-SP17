using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BattleSceneManager : MonoBehaviour
{
    void LeaveBattleScene()
    {
        SceneManager.UnloadScene("testScene");
    }
}
