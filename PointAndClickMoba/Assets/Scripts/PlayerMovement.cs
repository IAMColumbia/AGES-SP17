using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float speed = 5;
    [SerializeField]
    float turnSpeed = 13;

    [HideInInspector]
    public float speedBoostAmount = 0;

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        GetComponent<Rigidbody>().velocity = movement * speed;

        if (GetComponent<Rigidbody>().velocity != new Vector3(0, 0, 0))
        {
            Quaternion moveRotation = Quaternion.LookRotation(GetComponent<Rigidbody>().velocity);
            transform.rotation = Quaternion.Slerp(transform.rotation, moveRotation, turnSpeed * Time.deltaTime);
        }
    }

    float CalcSpeed()
    {
        return speed + speedBoostAmount;
    }
}
