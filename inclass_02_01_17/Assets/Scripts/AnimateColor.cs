using UnityEngine;
using System.Collections;

public class AnimateColor : MonoBehaviour {

    Material mat;
    float hue = 0;

    public float rate;

	// Use this for initialization
	void Start () {
        mat = GetComponent<MeshRenderer>().material;
	}
	
	// Update is called once per frame
	void Update () {
        UpdateHue(Time.deltaTime);

        mat.color = Color.HSVToRGB(hue, 1, 1);
	}

    void UpdateHue(float deltaTime)
    {
        hue += rate * deltaTime;
        if(hue > 1)
        {
            hue = 0;
        }
    }
}
