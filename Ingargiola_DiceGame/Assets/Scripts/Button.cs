using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour
{
    //[SerializeField] GameObject Cup;
    //[SerializeField] float shakeSpeed = 1;

    [SerializeField] GameObject RollCamera;



    private void Update()
    {
        
    }
    public void ShakeCup()
    {
        //transform.Translate
    }
    
    public void ActivateRollCamera()
    {
        RollCamera.SetActive(true);
    }
}
