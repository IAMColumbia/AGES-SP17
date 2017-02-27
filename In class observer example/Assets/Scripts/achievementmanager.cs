using UnityEngine;
using System.Collections;



public class achievementmanager : MonoBehaviour
{


    [SerializeField]
    GameObject AchievementPopUp;

    const int requiredKills = 3;



    private void Start()
    {
        AchievementPopUp.SetActive(false);
    }

    private void CheckForUnlockingAchievement()
    {
        if(enemy.NumberOfEnemiesThatHaveDied == requiredKills)
        {
            DisplayAchievement();
        }
    }

    private void OnEnable()
    {
        enemy.OnEnemyDied += CheckForUnlockingAchievement;

    }

    private void OnDisable()
    {
        enemy.OnEnemyDied -= CheckForUnlockingAchievement;

    }

    private void DisplayAchievement()
    {
        AchievementPopUp.SetActive(true);
        StartCoroutine(DismissAchievement());
    }

    private IEnumerator DismissAchievement()
    {
        yield return new WaitForSeconds(seconds: 2);

        AchievementPopUp.SetActive(false);
    }
}
