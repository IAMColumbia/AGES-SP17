using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class StatTracker : MonoBehaviour, Observer {

    public static StatTracker instance = null;

    [SerializeField]
    PlayerController playerA;

    [SerializeField]
    PlayerController playerB;

    Dictionary<PlayerController, Dictionary<statType, PlayerStat>> stats = new Dictionary<PlayerController, Dictionary<statType, PlayerStat>>();
    Dictionary<statType, PlayerStat> playerAStats = new Dictionary<statType, PlayerStat>();
    Dictionary<statType, PlayerStat> playerBStats = new Dictionary<statType, PlayerStat>();

    enum statType
    {
        score,
        shotsFired,
        hits,
        kills,
        accuracy,
        deaths
    }

    // Use this for initialization
    void Start () {
	    if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        playerA.Subscribe(this);
        playerB.Subscribe(this);

        DontDestroyOnLoad(this.gameObject);

        initStatDictionaries();
	}

    void initStatDictionaries()
    {
        stats.Add(playerA, playerAStats);
        stats.Add(playerB, playerBStats);

        foreach(KeyValuePair<PlayerController, Dictionary<statType, PlayerStat>> entry in stats)
        {
            Dictionary<statType, PlayerStat> trackedStats = entry.Value;


            //add all tracked stats to both player's dictionaries
            PlayerStat scoreStat = new PlayerStat(1, 0, false, "PTS");
            trackedStats.Add(statType.score, scoreStat);

            PlayerStat accuracyStat = new PlayerStat(1, 0, true, "ACC");
            trackedStats.Add(statType.accuracy, accuracyStat);

            PlayerStat killsStat = new PlayerStat(1, 0, false, "KILL");
            trackedStats.Add(statType.kills, killsStat);

            PlayerStat shotsStat = new PlayerStat(1, 0, false, "SHOT");
            trackedStats.Add(statType.shotsFired, shotsStat);

            PlayerStat hitsStat = new PlayerStat(1, 0, false, "HIT");
            trackedStats.Add(statType.hits, hitsStat);

            PlayerStat deathStat = new PlayerStat(1, 0, false, "DIE");
            trackedStats.Add(statType.deaths, deathStat);
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public List<PlayerStat>[] getStats()
    {
        finalizeStats();

        List<PlayerStat> aStatsList = new List<PlayerStat>();
        List<PlayerStat> bStatsList = new List<PlayerStat>();

        foreach(PlayerStat stat in playerAStats.Values)
        {
            aStatsList.Add(stat);
        }
        foreach (PlayerStat stat in playerBStats.Values)
        {
            bStatsList.Add(stat);
        }

        return new List<PlayerStat>[] { aStatsList, bStatsList };
    }

    public bool[] getWinners()
    {
        bool[] winners = new bool[] { false, false };

        float aScore = playerAStats[statType.score].PlayerScore;
        float bScore = playerBStats[statType.score].PlayerScore;

        if (aScore >= bScore)
        {
            winners[0] = true;
        }
        if (bScore >= aScore)
        {
            winners[1] = true;
        }

        return winners;
    }

    void finalizeStats()
    {
        //update non-event stats
        playerAStats[statType.accuracy].PlayerScore = playerAStats[statType.hits].PlayerScore / Mathf.Max(1, playerAStats[statType.shotsFired].PlayerScore); // the max is to prevent divide by zero
        playerBStats[statType.accuracy].PlayerScore = playerBStats[statType.hits].PlayerScore / Mathf.Max(1, playerBStats[statType.shotsFired].PlayerScore);

        playerAStats[statType.score].PlayerScore = playerA.Score;
        playerBStats[statType.score].PlayerScore = playerB.Score;

        //correct max values
        foreach (KeyValuePair<statType, PlayerStat> kvp in playerAStats)
        {
            PlayerStat trackedStatA = kvp.Value;
            PlayerStat trackedStatB = playerBStats[kvp.Key];

            if(trackedStatB != null)
            {
                if (!trackedStatA.IsPercentage)
                {
                    float max = trackedStatA.PlayerScore + trackedStatB.PlayerScore;

                    trackedStatA.MaxScore = max;
                    trackedStatB.MaxScore = max;
                }
            }
        }
    }

    public void Notify(Subject sender, Event e)
    {
        if (sender.GetType() == typeof(PlayerController)) {
            PlayerController player = (PlayerController)sender;
            Dictionary<statType, PlayerStat> targetStats = stats[player];

            switch (e)
            {
                case Event.firedBullet:
                    targetStats[statType.shotsFired].PlayerScore++;
                    break;
                case Event.playerDied:
                    targetStats[statType.deaths].PlayerScore++;
                    break;
                case Event.killedEnemy:
                    targetStats[statType.kills].PlayerScore++;
                    break;
                case Event.hitEnemy:
                    targetStats[statType.hits].PlayerScore++;
                    break;
            }
        }
    }
}
