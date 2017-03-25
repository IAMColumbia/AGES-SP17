using UnityEngine;
using System.Collections;

public class RotateTowardMouse : MonoBehaviour
{
    public string horizontalLookInput;
    public string verticalLookInput;

    void FixedUpdate()
    {
        float lookHorizontal = Input.GetAxis(horizontalLookInput);
        float lookVertical = Input.GetAxis(verticalLookInput);

        Vector3 lookDirection = new Vector3(lookHorizontal, 0.0f, lookVertical);

        if (lookDirection != new Vector3(0, 0, 0))
        {
            Quaternion lookRotation = Quaternion.LookRotation(lookDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 5 * Time.deltaTime);
        }
    }
}
