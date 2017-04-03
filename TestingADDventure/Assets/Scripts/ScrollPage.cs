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
        //float ScrollWheel = Input.GetAxis("Mouse ScrollWheel");

        pages.transform.position = Vector3.Lerp(new Vector3(0, -scrollAmount), new Vector3(0, scrollAmount + 3), GetComponent<Scrollbar>().value);
        //page2.transform.position = Vector3.Lerp(new Vector3(0, -scrollAmount - 20), new Vector3(0, scrollAmount - 20), GetComponent<Scrollbar>().value);
        //page1.transform.position += new Vector3(0, ScrollWheel * scrollAmount);
        //page2.transform.position += new Vector3(0, ScrollWheel * scrollAmount);
    }
}
