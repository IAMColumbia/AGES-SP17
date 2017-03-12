using UnityEngine;
using System.Collections;

public class RotateTowardMouse : MonoBehaviour
{
	void FixedUpdate()
    {
        float lookHorizontal = Input.GetAxis("LookHorizontal");
        float lookVertical = Input.GetAxis("LookVertical");

        Vector3 lookDirection = new Vector3(lookHorizontal, 0.0f, lookVertical);

        if (lookDirection != new Vector3(0, 0, 0))
        {
            Quaternion lookRotation = Quaternion.LookRotation(lookDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 5 * Time.deltaTime);
        }
    }
}
