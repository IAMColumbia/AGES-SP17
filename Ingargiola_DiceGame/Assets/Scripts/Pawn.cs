using UnityEngine;
using System.Collections;

public class Pawn : MonoBehaviour
{
    [SerializeField] string movementAxisName;
    [SerializeField] float pawnMovementX;
    [SerializeField] float pawnMovementY;
    [SerializeField] float pawnMovementZ;

    // Use this for initialization
    void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        PawnMovement();
        //transform.Translate(Vector3.right * Time.deltaTime);

    }

    public void PawnMovement()
    {
        //TODO: move based on number rolled
        //TODO: button to roll dice
        //TODO: controls for minigames

        if (Input.GetButtonDown(movementAxisName))
            if (Input.GetAxisRaw(movementAxisName) > 0)
                transform.Translate(pawnMovementX, pawnMovementY, pawnMovementZ);
            else if(Input.GetAxisRaw(movementAxisName) < 0)
                transform.Translate(pawnMovementX, pawnMovementY, -pawnMovementZ);


        //trying to store current position of pawn
        Vector3 currentPositino = transform.position;
       print(currentPositino);
       

    }

    public void PawnRollsDice()
    {

    }

}
