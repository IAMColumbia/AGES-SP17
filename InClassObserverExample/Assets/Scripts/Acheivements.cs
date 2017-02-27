using UnityEngine;
using System.Collections;

public class Acheivements : MonoBehaviour
{


    [SerializeField]
    GameObject acheivementPopup;

    const int requiredkills = 3;


    private void Start()
    {
        acheivementPopup.SetActive(false);

    }

    private void OnEnable()
    {
        Enemy.OnEnemyDied += CheckForUnlockingAcheivement;
    }

    private void OnDisable()
    {
        Enemy.OnEnemyDied -= CheckForUnlockingAcheivement;
    }

    private void CheckForUnlockingAcheivement()
    {
        if (Enemy.NumberOfEnemiesThatHaveDied == requiredkills)
        {
            DisplayAchievement();
        }
    }

    private void DisplayAchievement()
    {
        acheivementPopup.SetActive(true);
        StartCoroutine(DismissAcheivement());
    }

    private IEnumerator DismissAcheivement()
    {
        yield return new WaitForSeconds(seconds: 2);
        acheivementPopup.SetActive(false);
    }
}
