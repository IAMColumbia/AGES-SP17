using UnityEngine;
using System.Collections;

public class BGEffect : MonoBehaviour {

    [SerializeField]
    float minScale, maxScale, rate;

    [SerializeField]
    MeshRenderer[] m_renderers;

    float range;

	// Use this for initialization
	void Start () {
        range = maxScale - minScale;
	}
	
	// Update is called once per frame
	void Update () {
        float scale = minScale + (range/2) * (Mathf.Sin(2 * Mathf.PI * rate * Time.time) + 1f);

        foreach(MeshRenderer r in m_renderers)
        {
            r.material.mainTextureScale = new Vector2(1, scale);
        }
	}
}
