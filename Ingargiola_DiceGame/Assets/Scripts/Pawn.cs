using UnityEngine;
using System.Collections;
using System;

public class Pawn : MonoBehaviour
{
    private float pawnMovementX;
    private float pawnMovementY;
    private float pawnMovementZ;

    [SerializeField] string pawnMovementAxisName;

    public float pawnDefaultZMovement = 2;
    [HideInInspector]
    public float diceRoll;

    [HideInInspector]
    public bool canPawnMove;
    [HideInInspector]
    public bool canResetCup;



    // Use this for initialization
    void Start ()
    {

    }

    // Update is called once per frame
    void Update ()
    {
        //TODO: controls for minigames

        PawnMovement();
        //PawnCheckDiceRoll();
        //print(diceRoll);
    }//end Update

    private void PawnMovement()
    {
        pawnMovementZ = pawnDefaultZMovement * diceRoll;

        if (Input.GetButtonDown(pawnMovementAxisName) && canPawnMove == true)
            if (Input.GetAxisRaw(pawnMovementAxisName) > 0)
            {
                transform.Translate(pawnMovementX, pawnMovementY, pawnMovementZ);
                canResetCup = true;
            }

            //backwards movement
            //else if(Input.GetAxisRaw(pawnMovementAxisName) < 0)
            //    transform.Translate(pawnMovementX, pawnMovementY, -pawnMovementZ);

        
        //Vector3 currentPosition = transform.position;
       //print(currentPositino);
       
    }//end PawnMovement()
    
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
}//end Pawn Class
