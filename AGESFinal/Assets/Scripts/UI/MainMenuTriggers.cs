using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class MainMenuTriggers : MonoBehaviour {

    [SerializeField]
    private GameObject PanelToShow;

    [SerializeField]
    private GameObject NextSelectedGameObject;

    [SerializeField]
    private GameObject YButtonSprite;
    
    private EventSystem eventSystem;
    
    private void Start()
    {
        eventSystem = FindObjectOfType<EventSystem>();
    }
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        YButtonSprite.SetActive(true);
        if(Input.GetButtonDown("MainMenuButton"))
        {
            PanelToShow.SetActive(true);
            eventSystem.SetSelectedGameObject(null);
            FindObjectOfType<PlayerController>().enabled = false;
            eventSystem.SetSelectedGameObject(NextSelectedGameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        YButtonSprite.SetActive(false);
    }
    
}
