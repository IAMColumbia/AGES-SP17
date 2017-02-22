using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AnimateMenu : MonoBehaviour
{
    [SerializeField]
    Image m_titleText;

    Canvas m_canvas;

    [SerializeField]
    Camera m_cameraA, m_cameraB;

    float m_halfWidth;
    float m_fullHeight;

    // Use this for initialization
    void Start()
    {
        m_canvas = FindObjectOfType<Canvas>();

        m_halfWidth = m_cameraA.aspect * m_cameraA.orthographicSize * 2;
        m_fullHeight = m_cameraA.orthographicSize * 2;

        Debug.Log(m_halfWidth);
        Debug.Log(m_fullHeight);

        RectTransform m_canvasRect = m_canvas.GetComponent<RectTransform>();

        if(m_canvasRect == null)
        {
            throw new System.NullReferenceException("I don't know how you did it, but the canvas is missing its RectTransform");
        }

        m_canvasRect.sizeDelta = new Vector2(m_halfWidth * 2, m_fullHeight);
        m_canvasRect.position = new Vector3(m_halfWidth/2, 0, 0);

        m_titleText.rectTransform.offsetMin = new Vector2(m_halfWidth, m_titleText.rectTransform.offsetMin.y);
        m_titleText.rectTransform.offsetMax = new Vector2(m_halfWidth, m_titleText.rectTransform.offsetMax.y);
        m_titleText.rectTransform.sizeDelta = new Vector2(1,10000);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

