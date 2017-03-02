using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class AchievementManager : MonoBehaviour
{
    [SerializeField]
    GameObject achievementPopupPanel;

    private List<Achievement> allAchievements;
    
    private void Start()
    {
        achievementPopupPanel.SetActive(false);

        BuildAchievementList();
        InitializeAchievements();

        //StartCoroutine(TestAchievements());
    }


    private void BuildAchievementList()
    {
        throw new NotImplementedException();
    }

    private void InitializeAchievements()
    {
        foreach (var achievement in allAchievements)
        {
            achievement.Initialize();
        }
    }


    private void DisplayPopup()
    {
        achievementPopupPanel.SetActive(true);
        StartCoroutine(DismissPopup());
    }

    private IEnumerator DismissPopup()
    {
        yield return new WaitForSeconds(seconds: 2);

        achievementPopupPanel.SetActive(false);
    }

    private IEnumerator TestAchievements()
    {
        yield return new WaitForSeconds(seconds: 1);
        DisplayPopup();

        KillEnemiesAchievement achieve = new KillEnemiesAchievement();
    }



    private abstract class Achievement
    {
        public static event Action AchievementUnlocked;

        private bool isUnlocked;

        public bool IsUnlocked
        {
            get { return isUnlocked; }

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
            if (Enemy.NumberOfEnemiesThatHaveDied == requiredKills)
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
