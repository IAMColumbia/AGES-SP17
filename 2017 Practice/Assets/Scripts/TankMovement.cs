using UnityEngine;
using System.Collections;

public class TankMovement : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private float TankSpeed;
    [SerializeField]
    private float TurnSpeed;
    private float MovementInputValue;       
    private float TurnInputValue;

    private string TurnAxisName;
    private string MovementAxisName;

    void Awake ()
    {

        rb = GetComponent<Rigidbody>();
    }

    void Start ()
    {        
        MovementAxisName = "Vertical_P1";
        TurnAxisName = "Horizontal_P1";
    }

    void Update ()
    {
        
        MovementInputValue= Input.GetAxis(MovementAxisName);
        TurnInputValue = Input.GetAxis(TurnAxisName);
    }

    void FixedUpdate()
    {
        Move();
        Turn();
    }

    private void Move()
    {
       
        Vector3 movement = transform.forward * MovementInputValue * TankSpeed * Time.deltaTime;

       
        rb.MovePosition(rb.position + movement);
    }

    private void Turn()
    {

        float turn = TurnInputValue * TurnSpeed * Time.deltaTime;


        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);


       rb.MoveRotation(rb.rotation * turnRotation);
    }

}
