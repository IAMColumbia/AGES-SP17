﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class AchievementManager : MonoBehaviour
{
    [SerializeField] GameObject achievementPopup;
    const int requiredKills = 3;

    private void Start()
    {
        achievementPopup.SetActive(false);

        Enemy.OnEnemyDied += CheckForUnlockingAchievement;
    }

    private void CheckForUnlockingAchievement()
    {
        if (Enemy.NumberOfEnemiesThatHaveDied == requiredKills)
        {
            DisplayAchievement();
        }
    }

    private void DisplayAchievement()
    {
        achievementPopup.SetActive(true);
        StartCoroutine(DismissAchievement());
    }

    private IEnumerator DismissAchievement()
    {
        yield return new WaitForSeconds(seconds: 2);
        achievementPopup.SetActive(false);
    }
}
