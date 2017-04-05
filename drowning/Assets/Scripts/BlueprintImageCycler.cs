using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BlueprintImageCycler : MonoBehaviour {

    [SerializeField]
    Image m_imageFG, m_imageBG;

    [SerializeField]
    Color m_FGColor, m_BGColor;

    [SerializeField]
    float m_timeBetweenImages;

    [SerializeField]
    float m_scanTime;

    [SerializeField]
    Sprite[] m_images;

    [SerializeField]
    ImageJitter m_imageJitter = null;

    float m_timeOfLastChange = 0;

    float m_FGScanStartTime, m_FGScanFinishTime, m_BGScanStartTime, m_BGScanFinishTime;

    int m_imagesIndex = 0;

	// Use this for initialization
	void Start () {
        advanceToNextImage();
	}

    void advanceToNextImage()
    {
        //setup timestamps
        float now = Time.time;
        m_timeOfLastChange = now;
        m_FGScanStartTime = now;
        m_FGScanFinishTime = m_FGScanStartTime + m_scanTime;
        m_BGScanStartTime = now + m_timeBetweenImages / 2;
        m_BGScanFinishTime = m_BGScanStartTime + m_scanTime;

        //go to next image in array
        m_imagesIndex++;
        if(m_imagesIndex >= m_images.Length) { m_imagesIndex = 0; }
        m_imageFG.sprite = m_images[m_imagesIndex];
        m_imageBG.sprite = m_images[m_imagesIndex];
        m_imageFG.fillAmount = 0;
        m_imageBG.fillAmount = 0;
        m_imageBG.fillOrigin = 1; //from top

        if(m_imageJitter != null)
        {
            m_imageJitter.Jump();
        }
    }

    void updateScanning()
    {
        //fill both
        if(Time.time >= m_FGScanStartTime && Time.time <= m_FGScanFinishTime)
        {
            float progress = (Time.time - m_FGScanStartTime) / m_scanTime;

            m_imageFG.fillAmount = progress;
            m_imageBG.fillAmount = progress;
        }
        //hold for a moment
        else if(Time.time > m_FGScanFinishTime && Time.time < m_BGScanStartTime)
        {
            m_imageFG.fillAmount = 1;
            m_imageBG.fillAmount = 1;
        }
        //now sharpen
        else if(Time.time >= m_BGScanStartTime && Time.time <= m_BGScanFinishTime)
        {
            float progress = 1 - (Time.time - m_BGScanStartTime) / m_scanTime;

            m_imageBG.fillOrigin = 0; //from bottom

            m_imageBG.fillAmount = progress;
        }
        //hold again
        else if(Time.time > m_BGScanFinishTime && Time.time < m_timeOfLastChange + m_timeBetweenImages)
        {
            m_imageBG.fillAmount = 0;
        }
        //time to advance
        else if (Time.time >= m_timeOfLastChange + m_timeBetweenImages)
        {
            advanceToNextImage();
        }
    }
	
	// Update is called once per frame
	void Update () {
        updateScanning();
	}
}
