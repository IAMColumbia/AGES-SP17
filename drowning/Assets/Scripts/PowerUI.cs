using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PowerUI : MonoBehaviour {

    [SerializeField]
    Text PowerText, PercentageText;

    [SerializeField]
    Image FillImage;

    float t = 0;

    public float speed;
    public Color finishedColor;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        t += Time.deltaTime * speed;
        goToPercent(t);
	}

    void goToPercent(float percentage)
    {
        percentage = Mathf.Clamp01(percentage);
        PercentageText.text = percentage.ToString("###.000%");
        FillImage.fillAmount = percentage;

        if(percentage == 1)
        {
            PowerText.color = finishedColor;
            PercentageText.color = finishedColor;
        }
    }
}
