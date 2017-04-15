using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScrollPage : MonoBehaviour
{
    [SerializeField]
    GameObject pages;
    [SerializeField]
    float scrollAmount;
    [SerializeField]
    float scrollSensitivity = 0.5f;

    [HideInInspector]
    public bool canScroll { get; set; }

    void Start()
    {
        canScroll = false;
    }

    void Update()
    {
        float ScrollWheel = Input.GetAxis("Mouse ScrollWheel") * scrollSensitivity;
        GetComponent<Scrollbar>().value -= ScrollWheel;
    }

    public void Scroll()
    {
        pages.transform.position = Vector3.Lerp(new Vector3(0, -scrollAmount), new Vector3(0, scrollAmount), GetComponent<Scrollbar>().value);
    }
}
