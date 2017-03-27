using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPCController : MonoBehaviour
{
    #region Serialized Fields
    [SerializeField] float sensorLength; float speed;
    [SerializeField] float directionValue;
    [SerializeField] float turnValue;
    [SerializeField] float turnSpeed;
    #endregion

    public enum currentState { Standing, Walking, Turning };

    private Collider myCollider;
    private int turnDirection;
    private currentState curState;

    private Vector3 frontPos;
    private Vector3 leftPos;
    private Vector3 rightPos;
    private Vector3 backPos;

    private void Start()
    {
        sensorLength = 1.5f;
        speed = 5.0f;
        directionValue = 1.0f;
        turnValue = 0.0f;
        turnSpeed = 25.0f;
        myCollider = transform.GetComponent<Collider>();
        curState = currentState.Walking;
    }

    private void Update()
    {
        MoveAI();
        UpdateValues();
    }

    private void UpdateValues()
    {
        frontPos = new Vector3(transform.position.x, transform.position.y + .5f, transform.position.z);
        backPos = new Vector3(transform.position.x, transform.position.y + .5f, transform.position.z);
        leftPos = new Vector3(transform.position.x, transform.position.y + .5f, transform.position.z);
        rightPos = new Vector3(transform.position.x, transform.position.y + .5f, transform.position.z);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(frontPos, transform.forward * (sensorLength + transform.localScale.z));    //Forward sensor
        Gizmos.DrawRay(backPos, -transform.forward * (sensorLength + transform.localScale.z));   //Back sensor
        Gizmos.DrawRay(leftPos, -transform.right * (sensorLength + transform.localScale.x));     //Left sensor
        Gizmos.DrawRay(rightPos, transform.right * (sensorLength + transform.localScale.x));      //Right sensor
    }

    private void MoveAI()
    {
        RaycastHit hit;
        bool flag = false;  //Bool used to reset turn value if nothing is colliding. Flag is set to true on collision, and then reset here.

        if (Physics.Raycast(transform.position, transform.forward, out hit, (sensorLength + transform.localScale.z)))  //Front Sensor
        {
            if (hit.collider.tag != "Obstacle" && hit.collider.tag != "Tank" || hit.collider == myCollider)
            {
                return;
            }

            Debug.Log("Colliding");
            CheckTurnValue();

            flag = true;
        }

        //if (Physics.Raycast(transform.position, -transform.forward, out hit, (sensorLength + transform.localScale.z)))  //Back Sensor
        //{
        //    if (hit.collider.tag != "Obstacle" || hit.collider == myCollider)
        //    {
        //        return;
        //    }

        //    turnValue -= 1;

        //    flag = true;
        //}

        if (Physics.Raycast(transform.position, -transform.right, out hit, (sensorLength + transform.localScale.x)))  //Left Sensor
        {
            if (hit.collider.tag != "Obstacle" && hit.collider.tag != "Tank" || hit.collider == myCollider)
            {
                return;
            }

            if (turnValue >= 0)
            {
                turnValue += 1; //Turn right
            }
            flag = true;
        }

        if (Physics.Raycast(transform.position, transform.right, out hit, (sensorLength + transform.localScale.x)))  //Right Sensor
        {
            if (hit.collider.tag != "Obstacle" && hit.collider.tag != "Tank" || hit.collider == myCollider)
            {
                return;
            }

            if (turnValue <= 0)
            {
                turnValue -= 1; //Turn left
            }

            flag = true;
        }

        if (flag == false)  //If nothing has been hit this cycle, this sets the turn value to 0;
        {
            turnValue = 0;
        }

        transform.Rotate(Vector3.up * (turnSpeed * turnValue) * Time.deltaTime);
        //transform.rotation = Quaternion.LookRotation(Vector3.up * (turnSpeed * turnValue) * Time.deltaTime, transform.position);

        transform.position += transform.forward * (speed * directionValue) * Time.deltaTime;
    }

    private void CheckTurnValue()
    {
        if (curState == currentState.Walking)
        {
            int turnType = Random.Range(0, 2);

            if (turnType == 0)
            {
                turnDirection = 1;
            }
            else if (turnType == 1)
            {
                turnDirection = -1;
            }

            curState = currentState.Turning;
        }

        else if (curState == currentState.Turning)
        {
            turnValue += turnDirection;
        }
    }
}
