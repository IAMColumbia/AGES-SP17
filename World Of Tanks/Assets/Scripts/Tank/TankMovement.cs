using UnityEngine;

public class TankMovement : MonoBehaviour
{     
    public float Speed = 12f;            
    public float TurnSpeed = 180f;       

    [SerializeField] string verticalInput;
    [SerializeField] string horizontalInput;

    private string movementAxisName;     
    private string turnAxisName;         
    private Rigidbody rigidbody;         
    private float movementInputValue;    
    private float turnInputValue;              


    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }


    private void OnEnable ()
    {
        rigidbody.isKinematic = false;
        movementInputValue = 0f;
        turnInputValue = 0f;
    }


    private void OnDisable ()
    {
        rigidbody.isKinematic = true;
    }

    private void Update()
    {
        movementInputValue = Input.GetAxis(verticalInput);
        turnInputValue = Input.GetAxis(horizontalInput);
    }

    private void FixedUpdate()
    {
        Move();
        Turn();
    }

    private void Move()
    {
        Vector3 movement = -transform.forward * movementInputValue * Speed * Time.deltaTime;

        rigidbody.MovePosition(rigidbody.position + movement);
    }

    private void Turn()
    {
        float turn = turnInputValue * TurnSpeed * Time.deltaTime;

        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);

        rigidbody.MoveRotation(rigidbody.rotation * turnRotation);
    }

}