using UnityEngine;
using System.Collections;

public class ImageJitter : MonoBehaviour {

    [SerializeField]
    RectTransform m_transform;

    [SerializeField]
    float m_recenterSpeed, m_maxJumpDistance;

    Vector2 m_initialPosition;

	// Use this for initialization
	void Start () {
        m_initialPosition = m_transform.anchoredPosition;
	}
	
	// Update is called once per frame
	void Update () {
	    if(m_transform.anchoredPosition != m_initialPosition)
        {
            Vector2 newPosition = Vector2.Lerp(m_transform.anchoredPosition, m_initialPosition, Time.deltaTime * m_recenterSpeed);

            m_transform.anchoredPosition = newPosition;
        }
	}

    public void Jump()
    {
        Vector2 jumpLocation = new Vector2(Random.Range(-m_maxJumpDistance, m_maxJumpDistance) + m_initialPosition.x, Random.Range(-m_maxJumpDistance, m_maxJumpDistance) + m_initialPosition.y);

        m_transform.anchoredPosition = jumpLocation;
    }
}
