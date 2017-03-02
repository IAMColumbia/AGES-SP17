using UnityEngine;
using System.Collections;
using System;

public class AchievementManager : MonoBehaviour {

    [SerializeField]
    GameObject achievementPopupPanel;

    private List<Achievement> allAchievements;
	// Use this for initialization
	void Start ()
    {
        achievementPopupPanel.SetActive(false);
        //StartCoroutine(TestAchievements());
        allAchievements = new List<Achievement>();
        Achievement achievement = new KillEnemiesAchievement();

        Achievement.AchievementUnlocked += DisplayPopup;

	}
	
	// Update is called once per frame
	void Update ()
    {
        achievementPopupPanel.SetActive(true);
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

        private bool isUnlocked;

        public static event Action AchievementUnlocked;

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
