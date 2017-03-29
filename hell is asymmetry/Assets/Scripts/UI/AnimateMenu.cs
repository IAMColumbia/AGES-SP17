using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AnimateMenu : MonoBehaviour
{
    [SerializeField]
    Image m_titleTextA, m_titleTextB;

    Canvas m_canvas;

    [SerializeField]
    Camera m_cameraA, m_cameraB;

    [SerializeField]
    Toggle m_toggleA, m_toggleB;

    Canvas m_canvasA, m_canvasB;

    float m_Width;
    float m_Height;

    [SerializeField]
    AnimationCurve moveCurve;

    [SerializeField]
    Button startButton;

    [SerializeField]
    Slider[] progressSliders;

    [SerializeField]
    string sceneToLoad;

    float startTime;

    bool startingGame = false;

    AsyncOperation loadProgress = null;

    // Use this for initialization
    void Start()
    {


        m_Width = m_cameraA.aspect * m_cameraA.orthographicSize;
        m_Height = m_cameraA.orthographicSize * 2;

        Debug.Log(m_Width);
        Debug.Log(m_Height);

        SetTextPositionRelative(1);

        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        SetTextPositionRelative(moveCurve.Evaluate(Time.time - startTime));

        if (Input.GetButtonDown("FireA") && !startingGame)
        {
            m_toggleA.isOn = !m_toggleA.isOn;
        }

        if (Input.GetButtonDown("FireB") && !startingGame)
        {
            m_toggleB.isOn = !m_toggleB.isOn;
        }

        if (m_toggleA.isOn && m_toggleB.isOn && !startingGame)
        {
            BeginGame();
        }
    }

    void SetTextPositionRelative(float x)
    {
        m_titleTextA.rectTransform.anchoredPosition = new Vector2(m_cameraA.pixelWidth * x, 0);
        m_titleTextB.rectTransform.anchoredPosition = new Vector2(-m_cameraB.pixelWidth * x, 0);
    }

    void BeginGame()
    {
        startingGame = true;
        Debug.Log("STARTING GAME");

        loadProgress = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneToLoad);
    }

    private void OnGUI()
    {
        if(loadProgress != null)
        {
            foreach(Slider s in progressSliders)
            {
                s.value = loadProgress.progress;
            }
        }
    }
}

