using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SineSlider : MonoBehaviour {

    [SerializeField]
    Slider upperSlider, lowerSlider;

    public float Position
    {
        get
        {
            if(upperSlider.value >= lowerSlider.value)
            {
                return upperSlider.value;
            }
            else
            {
                return -lowerSlider.value;
            }
        }

        set
        {
            value = Mathf.Clamp(value, -1, 1);

            if(value >= 0)
            {
                upperSlider.value = value;
                lowerSlider.value = 0;
            }
            else
            {
                upperSlider.value = 0;
                lowerSlider.value = -value;
            }
        }
    }
}
