using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class AchievementManager : MonoBehaviour
{
    [SerializeField]
    GameObject achievementPopupPanel;

    List<Achievement> allAchievements;

    void Start()
    {
        achievementPopupPanel.SetActive(false);

        BuildAchievementList();

        InitializeAchievements();
    }

    void OnEnable()
    {
        Achievement.AchievementUnlocked += DisplayPopup;
    }

    void OnDisable()
    {
        Achievement.AchievementUnlocked -= DisplayPopup;
    }

    void BuildAchievementList()
    {
        allAchievements = new List<Achievement>();
        Achievement achievement = new KillEnemiesAchievement();
        allAchievements.Add(achievement);
    }

    void InitializeAchievements()
    {
        foreach (var achievement in allAchievements)
        {
            achievement.Initialize();
        }
    }

    void DisplayPopup()
    {
        achievementPopupPanel.SetActive(true);
        StartCoroutine(DismissPopup());
    }

    IEnumerator DismissPopup()
    {
        yield return new WaitForSeconds(seconds: 3);
        achievementPopupPanel.SetActive(false);
    }

    IEnumerator TestAchievements()
    {
        yield return new WaitForSeconds(seconds: 2);
        DisplayPopup();

        KillEnemiesAchievement achieve = new KillEnemiesAchievement();
    }

    abstract class Achievement
    {
        public static event Action AchievementUnlocked;

        bool isUnlocked;

        public bool IsUnlocked
        {
            get
            {
                return isUnlocked;
            }

            protected set
            {
                isUnlocked = value;

                if (isUnlocked)
                {
                    if (AchievementUnlocked != null)
                    {
                        AchievementUnlocked.Invoke();
                    }
                }
            }
        }

        public abstract void Evaluate();

        public virtual void Initialize()
        {

        }
    }

    class KillEnemiesAchievement : Achievement
    {
        const int requiredKills = 3;

        public override void Evaluate()
        {
            if (Enemy.NumberOfEnemiesKilled == requiredKills)
            {
                IsUnlocked = true;
            }
        }

        public override void Initialize()
        {
            base.Initialize();

            Enemy.EnemyDied += Evaluate;
        }
    }
}
