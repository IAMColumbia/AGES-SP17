using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour 
{
    public void Move(float x, float y)
    {
        transform.Translate(x, y, 0);
    }    
}
