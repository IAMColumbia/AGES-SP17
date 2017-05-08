using UnityEngine;
using System.Collections;
using System;

public class Pawn : MonoBehaviour
{
    [SerializeField] string pawnMovementAxisName;
    private float pawnMovementX;
    private float pawnMovementY;
    private float pawnMovementZ;

    public float pawnDefaultZMovement = 2;
    public float diceRoll;

    // Use this for initialization
    void Start ()
    {
        //pawnDefaultMovementZ = pawnMovementZ;
	}
	
	// Update is called once per frame
	void Update ()
    {
        //TODO: controls for minigames

        PawnMovement();
        //PawnCheckDiceRoll();

    }

    private void PawnMovement()
    {
        pawnMovementZ = pawnDefaultZMovement * diceRoll;

        if (Input.GetButtonDown(pawnMovementAxisName))
            if (Input.GetAxisRaw(pawnMovementAxisName) > 0)
                transform.Translate(pawnMovementX, pawnMovementY, pawnMovementZ);
            //else if(Input.GetAxisRaw(pawnMovementAxisName) < 0)
            //    transform.Translate(pawnMovementX, pawnMovementY, -pawnMovementZ);

        

        //trying to store current position of pawn
        Vector3 currentPositino = transform.position;
       //print(currentPositino);
       

    }

    public void PawnRollsDice()
    {
        //TODO: button to roll dice
        //pawnMovementZ = pawnDefaultMovementZ;
    }

    //private void PawnCheckDiceRoll()
    //{
    //    //TODO: move based on number rolled, if dice bool is true(run animation, move to location)
    //    if (diceSideOneIsDown == true)
    //        print("same thing");

    //    if (diceSideTwoIsDown == true)
    //        print("same thing");

    //    if (diceSideThreeIsDown == true)
    //        print("same thing");

    //    if (diceSideFourIsDown == true)
    //        print("same thing");

    //    if (diceSideFiveIsDown == true)
    //        print("same thing");

    //    if (diceSideSixIsDown == true)
    //        print("same thing");

    //}
}
