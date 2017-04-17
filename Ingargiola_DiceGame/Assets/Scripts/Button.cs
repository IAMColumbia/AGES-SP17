using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour
{
    [SerializeField] GameObject Cup;
    [SerializeField] float shakeSpeed = 1;


    private void Update()
    {
        ShakeCup();
    }
    public void ShakeCup()
    {
        //transform.Translate
    }
}
