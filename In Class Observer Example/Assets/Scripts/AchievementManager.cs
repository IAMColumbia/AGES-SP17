using UnityEngine;
using System.Collections;

public class AchievementManager : MonoBehaviour
{
    [SerializeField]
    GameObject achievementPopup;

    const int requiredKills = 3;
    
    private void Start()
    {
        achievementPopup.SetActive(false);
    }

    private void OnEnable()
    {
        Enemy.OnEnemyDie += CheckForUnlockingAchievement;
    }

    private void OnDisable()
    {
        Enemy.OnEnemyDie -= CheckForUnlockingAchievement;
    }

    private void CheckForUnlockingAchievement()
    {
        if(Enemy.NumberOfEnemiesThatHaveDied == requiredKills)
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
