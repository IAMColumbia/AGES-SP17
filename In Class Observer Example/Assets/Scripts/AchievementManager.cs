using UnityEngine;
using System.Collections;

public class AchievementManager : MonoBehaviour {

    [SerializeField]
    GameObject achievementPopUp;

    const int requiredKills = 3;

    private void Start()
    {
        achievementPopUp.SetActive(false);

        Enemy.OnEnemyDied += checkForUnlockeingAchievement;
    }

    private void checkForUnlockeingAchievement()
    {
        if (Enemy.NumberOfEnemiesThatHaveDied == requiredKills)
        {
            DisplayAchievement();
        }
    }
    private void DisplayAchievement()
    {
        achievementPopUp.SetActive(true);
        StartCoroutine(DismissAchievement());
    }

    private void OnEnable()
    {
        Enemy.OnEnemyDied += checkForUnlockeingAchievement;
    }

    private void OnDisable()
    {
        Enemy.OnEnemyDied -= checkForUnlockeingAchievement;
    }

    private IEnumerator DismissAchievement()
    {
        yield return new WaitForSeconds(seconds: 2);
    }
}
