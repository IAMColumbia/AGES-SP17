using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class AchieveManager : MonoBehaviour {

    // Use this for initialization
    [SerializeField]
    GameObject achievementPopUpPanel;

    private List<Achievement> allAchievements;
	void Start ()
    {
        achievementPopUpPanel.SetActive(false);
 
        BuildAchievementList();
        InitializeAchievements();

        Achievement.AchievementUnlocked += DisplayPopUp;
    }

    private void InitializeAchievements()
    {
        foreach(var achievement in allAchievements)
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

    // Update is called once per frame
    void DisplayPopUp () {
        achievementPopUpPanel.SetActive(true);
        StartCoroutine(DismissPopUpWindow());
	}
    IEnumerator DismissPopUpWindow()
    {
        yield return new WaitForSeconds(seconds: 2);
        achievementPopUpPanel.SetActive(false);
    }
    IEnumerator TestAchievemenets()
    {
        yield return new WaitForSeconds(seconds: 1);

            DisplayPopUp();
        KillEnemiesAchievement achieve = new KillEnemiesAchievement();

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
