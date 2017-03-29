using UnityEngine;
using System.Collections;

public class QuitGame : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<TankHealth>()) //if we can get the Tank's Health, we know it's a tank
        {
            Application.Quit();
        }
    }
}
