using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class AchievementManager : MonoBehaviour
{
    [SerializeField]
    GameObject achievementPopUpPanel;

    private List<Achievement> allAchievements;

    private void Start()
    {
        achievementPopUpPanel.SetActive(false);

        BuildAchievementList();
        InitializeAchievements();
        Achievement.AchievementUnlocked += DisplayPopUp;
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

    private void DisplayPopUp()
    {
        achievementPopUpPanel.SetActive(true);
        StartCoroutine(DismissPopUp());
    }

    private IEnumerator DismissPopUp()
    {
        yield return new WaitForSeconds(seconds: 3);

        achievementPopUpPanel.SetActive(false);
    }

    private IEnumerator TestAchievements()
    {
        yield return new WaitForSeconds(seconds: 1);
        achievementPopUpPanel.SetActive(true);
    }

    private abstract class Achievement
    {
        public static event Action AchievementUnlocked;

        private bool isUnlocked;

        public bool IsUnlocked
        {
            get { return isUnlocked;  }
            protected set
            {
                isUnlocked = value;

                if (isUnlocked)
                {
                    if (AchievementUnlocked != null)
                        AchievementUnlocked.Invoke();
                }
            }
        }

        public abstract void Evaluate();

        public virtual void Initialize()
        {

        }
    }

    private class KillEnemiesAchievement : Achievement
    {
        private const int requiredKills = 3;

        public override void Evaluate()
        {
            if (Enemy.numberOfEnemiesKilled == requiredKills)
            {
                IsUnlocked = true;
            }
        }

        public override void Initialize()
        {
            Enemy.EnemyDied += Evaluate;
        }
    }
}
