using UnityEngine;
using System.Collections;

public class Loader : MonoBehaviour
{
    [SerializeField]
    GameObject gameManager;

	void Awake()
    {
        if (GameManager.instance == null)
        {
            Instantiate(gameManager);
        }
	}
}
