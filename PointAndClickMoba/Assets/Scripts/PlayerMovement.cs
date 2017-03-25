using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float speed = 5;
    [SerializeField]
    float turnSpeed = 13;

    public string horizontalMoveInput;
    public string verticalMoveInput;

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis(horizontalMoveInput);
        float moveVertical = Input.GetAxis(verticalMoveInput);

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        GetComponent<Rigidbody>().velocity = movement * speed;

        if (GetComponent<Rigidbody>().velocity != new Vector3(0, 0, 0))
        {
            Quaternion moveRotation = Quaternion.LookRotation(GetComponent<Rigidbody>().velocity);
            transform.rotation = Quaternion.Slerp(transform.rotation, moveRotation, turnSpeed * Time.deltaTime);
        }
    }
}
