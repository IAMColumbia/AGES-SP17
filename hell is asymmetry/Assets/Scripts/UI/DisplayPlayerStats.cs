using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerStat
{
    public string Prefix = "SCO";
    public float MaxScore = 100;
    public float PlayerScore = 50;
    public bool IsPercentage = true;

    public PlayerStat()
    {

    }
    public PlayerStat(float _max, float _player, bool _percent = false, string _prefix = "SCO")
    {
        MaxScore = _max;
        PlayerScore = _player;
        IsPercentage = _percent;
        Prefix = _prefix;
    }
}

public class DisplayPlayerStats : MonoBehaviour {

    [SerializeField]
    float scoreDisplayDuration;

    [SerializeField]
    Transform statDisplayArea;

    [SerializeField]
    StatDisplayUnit statDisplayPrefab;

    [SerializeField]
    DisplayWinLose winLoseDisplay;

    [SerializeField]
    int playerStatBook = 0;

    [SerializeField]
    Toggle readyToggle;

    bool isWinner = false;

    bool canContinue = false;

    public bool ReadyToContinue = false;

    List<PlayerStat> playerStats = new List<PlayerStat>();
    List<StatDisplayUnit> statDisplays = new List<StatDisplayUnit>();

    DisplayPlayerStats[] friends;

	// Use this for initialization
	void Start () {
        //scoreSlider.maxValue = maxScore;
        readyToggle.gameObject.SetActive(false);

        loadStats();

        InstantiateDisplayUnits();

        StartCoroutine(displayPlayerScores(scoreDisplayDuration));

        friends = FindObjectsOfType<DisplayPlayerStats>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown(playerStatBook == 0 ? "FireA" : "FireB") && canContinue)
        {
            ReadyToContinue = !ReadyToContinue;
            readyToggle.isOn = ReadyToContinue;
        }

        bool timeToShowWinner = true;
        foreach(DisplayPlayerStats display in friends)
        {
            if (!display.ReadyToContinue)
            {
                timeToShowWinner = false;
                break;
            }
        }
        if (timeToShowWinner)
        {
            canContinue = false;
            winLoseDisplay.PlayerWon(isWinner);
        }
	}

    void InstantiateDisplayUnits()
    {
        foreach(PlayerStat stat in playerStats)
        {
            StatDisplayUnit statDisplay = Instantiate<StatDisplayUnit>(statDisplayPrefab);
            statDisplay.gameObject.transform.SetParent(statDisplayArea, false);
            statDisplays.Add(statDisplay);

            statDisplay.Init(stat.MaxScore, stat.PlayerScore, stat.Prefix, stat.IsPercentage);
        }
    }

    void loadTestStats()
    {
        playerStats.Add(new PlayerStat(200000, 178600, false, "PTS"));
        playerStats.Add(new PlayerStat(1, .76f, true, "ACC"));
        playerStats.Add(new global::PlayerStat(1000, 653, false, "SHOT"));
    }

    void loadStats()
    {
        playerStats = StatTracker.instance.getStats()[playerStatBook];
        isWinner = StatTracker.instance.getWinners()[playerStatBook];
    }

    IEnumerator displayPlayerScores(float duration)
    {
        yield return new WaitForSeconds(1);
        float t = 0;
        while (t < duration)
        {
            t += Time.deltaTime;
            SetStatDisplayProgress(t/duration);
            yield return 0;
        }

        SetStatDisplayProgress(1);

        readyToggle.gameObject.SetActive(true);
        canContinue = true;
    }

    void SetStatDisplayProgress(float t)
    {
        foreach(StatDisplayUnit statDisplay in statDisplays)
        {
            statDisplay.UpdateProgress(t);
        }
    }
}
