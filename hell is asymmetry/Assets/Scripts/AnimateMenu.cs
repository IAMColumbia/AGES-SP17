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

    [SerializeField]
    float titleWidthRatio;

    float m_halfWidth;
    float m_fullHeight;

    [SerializeField]
    AnimationCurve moveCurve;

    [SerializeField]
    Button startButton;

    float startTime;

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

        SetTextPositionRelative(1);
        m_titleText.rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, m_halfWidth, m_halfWidth * titleWidthRatio);

        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        SetTextPositionRelative(moveCurve.Evaluate(Time.time - startTime));
    }

    void SetTextPositionRelative(float x)
    {
        float textWidth = m_halfWidth * titleWidthRatio;
        float paddingLeft = (m_halfWidth - m_halfWidth * titleWidthRatio) / 2;
        m_titleText.rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, paddingLeft + (m_halfWidth - paddingLeft) * x, textWidth);
    }
}

