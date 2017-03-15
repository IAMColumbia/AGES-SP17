using UnityEngine;
using System.Collections;

public class BGEffect : MonoBehaviour {

    [SerializeField]
    float minScale, maxScale, rate;

    [SerializeField]
    bool scaleX, scaleY;

    [SerializeField]
    MeshRenderer[] m_renderers;

    float range, initXScale, initYScale;

	// Use this for initialization
	void Start () {
        range = maxScale - minScale;
        initXScale = m_renderers[0].material.mainTextureScale.x;
        initXScale = m_renderers[0].material.mainTextureScale.y;   
    }
	
	// Update is called once per frame
	void Update () {
        float scale = minScale + (range/2) * (Mathf.Sin(2 * Mathf.PI * rate * Time.time) + 1f);

        float xScale = scaleX ? scale : initXScale;
        float yScale = scaleY ? scale : initYScale;

        foreach(MeshRenderer r in m_renderers)
        {
            r.material.mainTextureScale = new Vector2(xScale, yScale);
        }
	}
}
