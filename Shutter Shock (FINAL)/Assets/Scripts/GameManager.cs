using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [SerializeField] List<string> levels;
    public int curLevel = 0;
    private bool canAdvanceLevel;

    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameManager>();
            }

            return _instance;
        }
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        canAdvanceLevel = true;
    }

    public void AdvanceLevel()
    {
        if (canAdvanceLevel)
        {
            canAdvanceLevel = false;
            curLevel++;
            SceneManager.LoadScene(levels[curLevel]);
        }

    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(levels[curLevel]);
    }
}
