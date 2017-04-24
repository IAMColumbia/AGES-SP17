using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenuTriggers : MonoBehaviour {

    [SerializeField]
    private GameObject PanelToShow;

    [SerializeField]
    private GameObject NextSelectedGameObject;
    
    private EventSystem eventSystem;
    
    private void Start()
    {
        eventSystem = FindObjectOfType<EventSystem>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(Input.GetButtonDown("MainMenuButton"))
        {
            PanelToShow.SetActive(true);
            collision.gameObject.SetActive(false);
            eventSystem.SetSelectedGameObject(NextSelectedGameObject);
        }
    }
}
