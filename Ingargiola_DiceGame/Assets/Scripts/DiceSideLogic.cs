using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class DiceSideLogic : Pawn
{
    [SerializeField] Pawn pawnRefernce;
    public Text rollTextObject;//dice logic
    public string rollTextDefault = "Roll the dice!";//dice logic
    public string rolledText = "You rolled a ";//dice logic
    // Use this for initialization
    void Start()
    {
        rollTextObject.text = rollTextDefault;
        //StartCoroutine(WaitUntilDiceStops());

    }

    // Update is called once per frame
    void Update()
    {
    }
    
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Dice")
        {
            print(col.gameObject.name);
            switch (col.gameObject.name)
            {
                case "DiceSideCollision (1)": //Side 6 is up
                    rollTextObject.text = rolledText + "6!";
                    pawnRefernce.diceRoll = 6;
                    //print(pawnRefernce.pawnMovementZ);

                    //diceSideOneIsDown = true;
                    //diceSideTwoIsDown = false;
                    //diceSideThreeIsDown = false;
                    //diceSideFourIsDown = false;
                    //diceSideFiveIsDown = false;
                    //diceSideSixIsDown = false;
                    break;

                case "DiceSideCollision (2)": //Side 5 is up
                    rollTextObject.text = rolledText + "5!";
                    pawnRefernce.diceRoll = 5;
                    //print(pawnRefernce.pawnMovementZ);

                    //diceSideTwoIsDown = true;
                    //diceSideOneIsDown = false;
                    //diceSideThreeIsDown = false;
                    //diceSideFourIsDown = false;
                    //diceSideFiveIsDown = false;
                    //diceSideSixIsDown = false;
                    break;

                case "DiceSideCollision (3)": //Side 4 is up
                    rollTextObject.text = rolledText + "4!";
                    pawnRefernce.diceRoll = 4;
                    //print(pawnRefernce.pawnMovementZ);

                    //diceSideThreeIsDown = true;
                    //diceSideOneIsDown = false;
                    //diceSideTwoIsDown = false;
                    //diceSideFourIsDown = false;
                    //diceSideFiveIsDown = false;
                    //diceSideSixIsDown = false;
                    break;

                case "DiceSideCollision (4)": //Side 3 is up
                    rollTextObject.text = rolledText + "3!";
                    pawnRefernce.diceRoll = 3;
                    //print(pawnRefernce.pawnMovementZ);

                    //diceSideFourIsDown = true;
                    //diceSideOneIsDown = false;
                    //diceSideTwoIsDown = false;
                    //diceSideThreeIsDown = false;
                    //diceSideFiveIsDown = false;
                    //diceSideSixIsDown = false;
                    break;

                case "DiceSideCollision (5)": //Side 2 is up
                    rollTextObject.text = rolledText + "2!";
                    pawnRefernce.diceRoll = 2;
                    //print(pawnRefernce.pawnMovementZ);

                    //diceSideFiveIsDown = true;
                    //diceSideOneIsDown = false;
                    //diceSideTwoIsDown = false;
                    //diceSideThreeIsDown = false;
                    //diceSideFourIsDown = false;
                    //diceSideSixIsDown = false;
                    break;

                case "DiceSideCollision (6)": //Side 1 is up
                    rollTextObject.text = rolledText + "1!";
                    pawnRefernce.diceRoll = 1;
                    //print(pawnRefernce.pawnMovementZ);
                    
                    // diceSideSixIsDown = true;
                    //diceSideOneIsDown = false;
                    //diceSideTwoIsDown = false;
                    //diceSideThreeIsDown = false;
                    //diceSideFourIsDown = false;
                    //diceSideFiveIsDown = false;
                    break;

                default:
                    break;
            }//end switch
        }//end if
         //Debug.Log(this.name);
    }//end OnTriggerStay

    //private void ThisSideIsDown()
    //{
    //    if (diceSideOneIsDown == true)
    //        print("You rolled a 6");
    //        rollText.text = "6";

    //    if (diceSideTwoIsDown == true)
    //        print("You rolled a 5");
    //        rollText.text = "5";

    //    if (diceSideThreeIsDown == true)
    //        print("You rolled a 4");
    //        rollText.text = "4";

    //    if (diceSideFourIsDown == true)
    //        print("You rolled a 3");
    //        rollText.text = "3";

    //    if (diceSideFiveIsDown == true)
    //        print("You rolled a 2");
    //        rollText.text = "2";

    //    if (diceSideSixIsDown == true)
    //        print("You rolled a 1");
    //        rollText.text = "1";

    //}//end ThisSideIsDown()

    //private IEnumerator WaitUntilDiceStops()
    //{
    //    yield return new WaitForSeconds(5);
    //    ThisSideIsDown();
    //}//end Coroutine WaitUntilDiceStops()


}//end DiceSideLogic Class






























//using UnityEngine;
//using System.Collections;

//public class DiceSideLogic : MonoBehaviour {

//	// Use this for initialization
//	void Start () {

//	}

//	// Update is called once per frame
//	void Update () {

//	}
//    void OnTriggerStay(Collider col)
//    {
//        //TODO: adjust collider sizes so that only one is touching
//        if (col.tag == "Floor")
//            Debug.Log(this.name);
//        //switch if 
//    }

//}
