using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DistractionManager : MonoBehaviour
{
    [SerializeField]
    EnergyMeter energyManager;
    [SerializeField]
    GameObject rhythmGameDistraction;
    [SerializeField]
    float rhythmGameEnergyLevel;
    [SerializeField]
    Slider rhythmGameScoreSlider;

    public int rhythmGameScore = 0;

    int rhythmGameScoreGoal = 6;

    void Update()
    {
        ManageRhythmGame();
    }

    void ManageRhythmGame()
    {
        if (energyManager.energyLeft == rhythmGameEnergyLevel)
        {
            rhythmGameDistraction.SetActive(true);

            rhythmGameScoreSlider.value = rhythmGameScore;

            if (rhythmGameScore == rhythmGameScoreGoal)
            {
                rhythmGameDistraction.SetActive(false);
            }
        }
    }
}
