using UnityEngine;

public class UIDirectionControl : MonoBehaviour
{
    Quaternion m_RelativeRotation;

    public bool m_UseRelativeRotation = true;

    void Start()
    {
        m_RelativeRotation = transform.parent.localRotation;
    }

    void Update()
    {
        if (m_UseRelativeRotation)
        {
            transform.rotation = m_RelativeRotation;
        }
    }
}
