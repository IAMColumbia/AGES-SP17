using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerStat
{
    public string prefix = "SCO";
    public float max = 100;
    public float player = 50;
    public bool percent = true;

    public PlayerStat()
    {

    }
    public PlayerStat(float _max, float _player, bool _percent = false, string _prefix = "SCO")
    {
        max = _max;
        player = _player;
        percent = _percent;
        prefix = _prefix;
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

    public bool isWinner;

    List<PlayerStat> playerStats = new List<PlayerStat>();
    List<StatDisplayUnit> statDisplays = new List<StatDisplayUnit>();

	// Use this for initialization
	void Start () {
        //scoreSlider.maxValue = maxScore;

        loadTestStats();

        InstantiateDisplayUnits();

        StartCoroutine(displayPlayerScores(scoreDisplayDuration));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void InstantiateDisplayUnits()
    {
        foreach(PlayerStat stat in playerStats)
        {
            StatDisplayUnit statDisplay = Instantiate<StatDisplayUnit>(statDisplayPrefab);
            statDisplay.gameObject.transform.SetParent(statDisplayArea, false);
            statDisplays.Add(statDisplay);

            statDisplay.Init(stat.max, stat.player, stat.prefix, stat.percent);
        }
    }

    void loadTestStats()
    {
        playerStats.Add(new PlayerStat(200000, 178600, false, "PTS"));
        playerStats.Add(new PlayerStat(1, .76f, true, "ACC"));
        playerStats.Add(new global::PlayerStat(1000, 653, false, "SHOT"));
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

        yield return new WaitForSeconds(2);

        winLoseDisplay.PlayerWon(isWinner);
    }

    void SetStatDisplayProgress(float t)
    {
        foreach(StatDisplayUnit statDisplay in statDisplays)
        {
            statDisplay.UpdateProgress(t);
        }
    }
}
