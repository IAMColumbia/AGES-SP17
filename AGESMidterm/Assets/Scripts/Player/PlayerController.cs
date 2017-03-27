using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float PlayerNumber;
    public float MoveSpeed;
    public float TurretTurningSpeed = 50;
    public GameObject Turret;

    private float InputAxisX;
    private float InputAxisY;
    private float turnInputAxisLeft;
    private float turnInputAxisRight;

    void Update ()
    {

        GetAxis();
        Movement();
        RotateTurret();
        
    }

    private void Movement()
    {
        Vector3 movement = new Vector3(InputAxisY, 0, InputAxisX);

        transform.Translate(movement * MoveSpeed * Time.deltaTime);
    }

    private void RotateTurret()
    {
        Vector3 turnRight = new Vector3(0, turnInputAxisRight, 0);
        Vector3 turnLeft = new Vector3(0, turnInputAxisLeft, 0);
        
        Turret.transform.RotateAround(Turret.transform.parent.position, turnRight, TurretTurningSpeed * Time.deltaTime);

        Turret.transform.RotateAround(Turret.transform.parent.position, turnLeft * -1, TurretTurningSpeed * Time.deltaTime);
            
    }

    private void GetAxis()
    {
        InputAxisX = Input.GetAxisRaw("Horizontal" + PlayerNumber);
        InputAxisY = Input.GetAxisRaw("Vertical" + PlayerNumber);

        turnInputAxisLeft = Input.GetAxisRaw("TurretTurnLeft" + PlayerNumber);
        turnInputAxisRight = Input.GetAxisRaw("TurretTurnRight" + PlayerNumber);
    }
}
