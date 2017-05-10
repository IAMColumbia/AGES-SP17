using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PowerUI : MonoBehaviour {

    [SerializeField]
    Text PowerText, PercentageText;

    [SerializeField]
    Image FillImage, BGFillImage;

    float t = 0;

    public float speed, BGFillSpeed;
    public Color finishedColor;

    public bool IsAtFullPower
    {
        get
        {
            return t >= 1;
        }
    }

    public float Power
    {
        get
        {
            return t;
        }
    }

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (!IsAtFullPower)
        {
            t += Time.deltaTime * speed;
            goToPercent(t);
            updateBGFill();
        }
	}

    void updateBGFill()
    {
        if(t > BGFillImage.fillAmount)
        {
            BGFillImage.fillAmount = t;
        }

        else
        {
            BGFillImage.fillAmount = Mathf.Lerp(BGFillImage.fillAmount, t, BGFillSpeed);
        }
    }

    public void goToPercent(float percentage)
    {
        percentage = Mathf.Clamp01(percentage);
        t = percentage;
        PercentageText.text = percentage.ToString("###.000%");
        FillImage.fillAmount = percentage;

        if(percentage == 1)
        {
            PowerText.color = finishedColor;
            PercentageText.color = finishedColor;
        }
    }
}
