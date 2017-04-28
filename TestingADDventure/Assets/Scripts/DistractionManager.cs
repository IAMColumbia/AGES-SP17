using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class DistractionManager : MonoBehaviour
{
    [SerializeField]
    EnergyMeter energyManager;
    [SerializeField]
    GameObject rhythmGameDistraction;
    [SerializeField]
    Slider rhythmGameScoreSlider;
    [SerializeField]
    Slider tappingSlider;
    [SerializeField]
    Button tappingStopButton;
    [SerializeField]
    GameObject tappingCoverImage;
    [SerializeField]
    bool rhythmGameEnabled;
    [SerializeField]
    bool tappingSliderEnabled;

    public int rhythmGameScore = 0;

    bool canTap = true;
    int rhythmGameScoreGoal = 6;
    float rhythmGameEnergyLevel = 3;
    float tappingEnergyLevel = 9;

    void Update()
    {
        if (rhythmGameEnabled)
        {
            ManageRhythmGame();
        }

        if (tappingSliderEnabled)
        {
            ManageTappingSlider();
        }
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

    void ManageTappingSlider()
    {
        if (energyManager.energyLeft <= tappingEnergyLevel && canTap)
        {
            tappingSlider.gameObject.SetActive(true);

            if (tappingSlider.value >= 0 && tappingSlider.value < 100)
            {
                InvokeRepeating("IncreaseSlider", 2, 0.05f);
            }

            InvokeRepeating("TapEffects", 0, 0.3f);

            canTap = false;
        }

        if (tappingSlider.value == 100)
        {
            if (IsInvoking("IncreaseSlider"))
            {
                CancelInvoke("IncreaseSlider");
            }

            tappingCoverImage.SetActive(true);
            tappingStopButton.gameObject.SetActive(true);
        }
    }

    void IncreaseSlider()
    {
        tappingSlider.value++;
    }

    void TapEffects()
    {
        Debug.Log("tap");
        //play tap sound
        //sound volume = slider.value
        //increase size animation
    }

    public void StopButtonPressed()
    {
        tappingSlider.value = 0;
        tappingStopButton.gameObject.SetActive(false);
        tappingCoverImage.SetActive(false);
        canTap = true;
    }
}
