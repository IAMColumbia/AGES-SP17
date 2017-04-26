using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    float levelStartDelay = 2f;
    [SerializeField]
    float turnDelay = 0.1f;

    public static GameManager instance = null;
    public BoardManager boardScript;

    public int playerFoodPoints = 100;

    [HideInInspector]
    public bool isPlayerTurn = true;

    Text levelText;
    GameObject levelImage;

    int level = 1;
    bool doingSetup;

    List<Enemy> enemies;
    bool enemiesMoving;

    // Singleton Pattern
	void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        enemies = new List<Enemy>();
        boardScript = GetComponent<BoardManager>();

        InitGame();
	}

    private void OnLevelWasLoaded(int index)
    {
        level++;

        InitGame();
    }

    void InitGame()
    {
        doingSetup = true;

        levelImage = GameObject.Find("LevelImage");
        levelText = GameObject.Find("LevelText").GetComponent<Text>();
        levelText.text = "Day " + level;
        levelImage.SetActive(true);
        Invoke("HideLevelImage", levelStartDelay);

        enemies.Clear();
        boardScript.SetupScene(level);
    }

    void HideLevelImage()
    {
        levelImage.SetActive(false);
        doingSetup = false;
    }

    void Update()
    {
        if (isPlayerTurn || enemiesMoving || doingSetup)
        {
            return;
        }

        StartCoroutine(MoveEnemies());
    }

    public void GameOver()
    {
        levelText.text = "After " + level + " days, you starved.";
        levelImage.SetActive(true);
        enabled = false;
    }

    public void AddEnemiesToList(Enemy script)
    {
        enemies.Add(script);
    }

    IEnumerator MoveEnemies()
    {
        enemiesMoving = true;

        yield return new WaitForSeconds(turnDelay);

        if (enemies.Count == 0)
        {
            yield return new WaitForSeconds(turnDelay);
        }

        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].MoveEnemy();

            yield return new WaitForSeconds(enemies[i].moveTime);
        }

        isPlayerTurn = true;
        enemiesMoving = false;
    }
}
