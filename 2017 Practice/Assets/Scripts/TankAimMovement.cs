using UnityEngine;
using System.Collections;

public class TankAimMovement : MonoBehaviour {
    private string AimAxisName;
    private float moveAim;


    [SerializeField]
    private float AimSpeed;

	// Use this for initialization
	void Start () {
        AimAxisName = "VerticalAIM_P1";
    }
	
    void Update ()
    {
        moveAim = Input.GetAxis(AimAxisName);

    }
	// Update is called once per frame
	void FixedUpdate () {

        AimTurn();
        Debug.Log(moveAim.ToString());

    }

    private void AimTurn()
    {
                     
        float turn = moveAim * AimSpeed * Time.deltaTime;


        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);


        transform.Rotate(Vector3.up, turn);
    }
}
