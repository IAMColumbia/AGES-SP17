using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpEmission2 : MonoBehaviour
{
    [SerializeField]
    float scrollX, scrollY;
    Material material;
    // Use this for initialization
    void Start()
    {
        material = GetComponent<Renderer>().material;
        material.SetTextureOffset("_MainTex", Vector2.zero);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 offset = new Vector2(material.GetTextureOffset("_MainTex").x + Time.deltaTime * scrollX, material.GetTextureOffset("_MainTex").y + Time.deltaTime * scrollY);
        material.SetTextureOffset("_MainTex", offset);
    }
}