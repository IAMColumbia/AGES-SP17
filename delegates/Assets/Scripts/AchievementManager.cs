using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class AchievementManager : MonoBehaviour {

    [SerializeField]
    GameObject achievementPopupPanel;

    [SerializeField]
    Text achievementText;

    List<Achievement> allAchievements;

	// Use this for initialization
	void Start () {
        achievementPopupPanel.SetActive(false);

        allAchievements = new List<Achievement>();

        Achievement killAchievement = new KillEnemiesAchievement();
        killAchievement.Initialize("KILLED 3");
        allAchievements.Add(killAchievement);
	}

    void OnEnable()
    {
        Achievement.AchievementUnlocked += AchievementUnlocked;
    }

    void OnDisable()
    {
        Achievement.AchievementUnlocked -= AchievementUnlocked;
    }

    void DisplayPopup()
    {
        achievementPopupPanel.SetActive(true);
        StartCoroutine(closePopupAfterTime(2));
    }

    void HidePopup()
    {
        achievementPopupPanel.SetActive(false);
    }

    void AchievementUnlocked(Achievement achievement)
    {
        achievementText.text = achievement.AchievementText;
        DisplayPopup();
    }

    IEnumerator closePopupAfterTime(float t)
    {
        yield return new WaitForSeconds(t);
        HidePopup();
    }
}

abstract class Achievement {

    public static event Action<Achievement> AchievementUnlocked;

    protected bool isUnlocked;
    public bool IsUnlocked {
        get {
            return isUnlocked;
        }
        protected set {
            isUnlocked = value;
            AchievementUnlocked.Invoke(this);
        }
    }

    protected string achievementText;
    public string AchievementText { get { return achievementText; } }

    abstract public void Evaluate();

    public virtual void Initialize(string text)
    {
        achievementText = text;
    }
}

class KillEnemiesAchievement : Achievement
{
    int targetNumKills = 3;

    public override void Evaluate()
    {
        if(Enemy.NumEnemiesThatHaveDied >= targetNumKills && !IsUnlocked)
        {
            IsUnlocked = true;
        }
    }

    public override void Initialize(string text)
    {
        achievementText = text;
        Enemy.enemyDied += Evaluate;
    }
}
