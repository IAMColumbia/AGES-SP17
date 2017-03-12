using UnityEngine;
using System.Collections;

public class DestroyObject : MonoBehaviour
{
    [SerializeField]
    float delay;

    void Awake()
    {
        Destroy(gameObject, delay);
    }
}
