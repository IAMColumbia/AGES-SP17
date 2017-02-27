using UnityEngine;
using System.Collections;

public class AcheivmentManager : MonoBehaviour
{
    public delegate string delegateTest(string s);

    public delegateTest myDelegate;

    [SerializeField]
    GameObject achievmentPopup;

    const int requiredKills = 3;

    string DelegateExampleMethod(string s)
    {
        return s + " new text";
    }

    void Start()
    {
        achievmentPopup.SetActive(false);
    }

    void OnEnable()
    {
        Enemy.OnEnemyDied += CheckForUnlockingAchievment;
    }

    void OnDisable()
    {
        Enemy.OnEnemyDied -= CheckForUnlockingAchievment;
    }

    void CheckForUnlockingAchievment()
    {
        if (Enemy.NumberOfEnemiesThatHaveDied == requiredKills)
        {
            DisplayAcheivment();
        }
    }

    void DisplayAcheivment()
    {
        achievmentPopup.SetActive(true);
        StartCoroutine(DismissAchievment());
    }

    IEnumerator DismissAchievment()
    {
        yield return new WaitForSeconds(seconds: 2);
        achievmentPopup.SetActive(false);
    }
}
