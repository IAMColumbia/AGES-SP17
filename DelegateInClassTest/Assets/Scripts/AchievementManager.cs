using UnityEngine;
using System.Collections;

public class AchievementManager : MonoBehaviour
{
    [SerializeField] GameObject AchievmentPanel;
    const int requiredKills = 3;

    private void Start()
    {
        AchievmentPanel.SetActive(false);

    }

    private void OnEnable()
    {
        Enemy.OnEnemyDied += CheckForUnlockingAchievement;
    }
    private void OnDisable()
    {
        Enemy.OnEnemyDied -= CheckForUnlockingAchievement;
    }

    private void DisplayAchievement()
    {
        StartCoroutine(ShowAchievement());
    }

    IEnumerator ShowAchievement()
    {
        AchievmentPanel.SetActive(true);
        yield return new WaitForSeconds(seconds: 2);
        AchievmentPanel.SetActive(false);
    }

    private void CheckForUnlockingAchievement()
    {
        if (Enemy.NumberOfEnemiesThatHaveDied == requiredKills)
        {
            DisplayAchievement();
        }
    }
}
