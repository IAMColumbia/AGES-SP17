using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class LoadScene : MonoBehaviour {

    [SerializeField]
    string sceneToLoad;

    [SerializeField]
    Material buttonMaterial, pushedButtonMaterial;

    [SerializeField]
    Renderer buttonRenderer;

    bool isTouchingButton, buttonPressed;

    WaitForSeconds waitForSceneToLoad = new WaitForSeconds(1.5f);
    void Update()
    {
        if (isTouchingButton == true)
        {
            if (Input.GetButtonDown("Interact"))
            {
                StartCoroutine(LoadingScene());
            }
        }

        ChangeButtonColor();
    }



    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            isTouchingButton = true;
            Debug.Log("Player is touching button? " + isTouchingButton.ToString());
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Player")
        {
            isTouchingButton = false;
            Debug.Log("Player is touching button? " + isTouchingButton.ToString());
        }
    }

    private void ChangeButtonColor()
    {
        if(buttonPressed == true)
        {
            buttonRenderer.material.color = pushedButtonMaterial.color;
        }
        else if(buttonPressed == false)
        {
            buttonRenderer.material.color = buttonMaterial.color;
        }
    }

    private IEnumerator LoadingScene()
    {
        Debug.Log(sceneToLoad);
        buttonPressed = true;
        yield return waitForSceneToLoad;
        SceneManager.LoadScene(sceneToLoad);
    }
}
