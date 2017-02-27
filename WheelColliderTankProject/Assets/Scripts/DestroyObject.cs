using UnityEngine;
using System.Collections;

public class DestroyObject : MonoBehaviour
{
    [SerializeField]
    float delay = 1;

    void Start()
    {
        Destroy(gameObject, delay);
    }
}
