using UnityEngine;
using System.Collections;

public class AnimateColor2 : MonoBehaviour {

    Material mat;
    float hue = 0;

    public Transform controller;

    // Use this for initialization
    void Start()
    {
        mat = GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHue();

        mat.color = Color.HSVToRGB(hue, 1, 1);
    }

    void UpdateHue()
    {
        float rotation = controller.eulerAngles.y;
        hue = rotation / 360;
    }
}
