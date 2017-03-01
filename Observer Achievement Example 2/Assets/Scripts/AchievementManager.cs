using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class AchievementManager : MonoBehaviour 
{
    private List<Achievement> allAchievements;

	// Use this for initialization
	void Start ()
    {
        BuildAchievementList();

        InitializeAchievements();
    }

    private void InitializeAchievements()
    {
        foreach (var achievement in allAchievements)
        {
            achievement.Initialize();
        }
    }

    private void BuildAchievementList()
    {
        allAchievements = new List<Achievement>();

        Achievement achievement = new KillEnemiesAchievement();
        allAchievements.Add(achievement);
    }

    private class KillEnemiesAchievement : Achievement
    {
        private const int requiredKills = 3;

        public override void Evaluate()
        {
            if (!IsUnlocked)
            {
                if (Enemy.NumberKilled == requiredKills)
                {
                    IsUnlocked = true;

                    Debug.Log("Achievement Unlocked!");
                    base.Evaluate();
                }
            }
        }

        public override void Initialize()
        {
            Enemy.EnemyDied += Evaluate;
        }
    }

    private abstract class Achievement
    {
        public bool IsUnlocked { get; protected set; }
        public abstract void Initialize();

        public virtual void Evaluate()
        {
            if (IsUnlocked)
            {
                // TODO: display popup achievement panel!
            }
        }

    }
}
