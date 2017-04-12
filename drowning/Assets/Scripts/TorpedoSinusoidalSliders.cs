using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TorpedoSinusoidalSliders : MonoBehaviour {

    [SerializeField]
    float frequency;

    float theta = 0;

    [SerializeField]
    int numSliders;

    [SerializeField]
    Transform sliderBox;

    [SerializeField]
    SineSlider sliderPrefab;

    List<SineSlider> sliders = new List<SineSlider>();

	// Use this for initialization
	void Start () {
        initSliders();
	}
	
    void initSliders()
    {
        for(int i = sliders.Count - 1; i >= 0; i--)
        {
            SineSlider slider = sliders[i];

            sliders.RemoveAt(i);

            Destroy(slider.gameObject);
        }

        sliders.Clear();

        for(int i = 0; i < numSliders; i++)
        {
            SineSlider newSlider = Instantiate<SineSlider>(sliderPrefab);

            newSlider.transform.SetParent(sliderBox, false);

            newSlider.Position = 0;

            sliders.Add(newSlider);
        }
    }

	// Update is called once per frame
	void Update () {
	
	}

    void FixedUpdate()
    {
        float sliderOffset = Mathf.PI * 2 / numSliders;

        theta += frequency * Mathf.PI * 2 * Time.deltaTime;

        if(theta > Mathf.PI * 2)
        {
            theta = 0;
        }

        for(int i = 0; i < numSliders; i++)
        {
            SineSlider currentSlider = sliders[i];

            currentSlider.Position = Mathf.Sin(theta + sliderOffset * i);
        }
    }
}
