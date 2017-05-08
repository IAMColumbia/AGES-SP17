using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour
{

    [SerializeField]
    string SceneToLoad;

    public bool isActive;

    void OnCollisionEnter(Collision collision)
    {
        if (isActive)
        {
            if (collision.gameObject.tag == "Player")
            {
                LoadingScene.LoadNewScene(SceneToLoad);
            }
        }
    }
}
