using UnityEngine;
using System.Collections;
using System;

public class DiceSideLogic : MonoBehaviour
{
    #region Dice Side Bools
    bool diceSideOneIsDown = false;
    bool diceSideTwoIsDown = false;
    bool diceSideThreeIsDown = false;
    bool diceSideFourIsDown = false;
    bool diceSideFiveIsDown = false;
    bool diceSideSixIsDown = false;
    #endregion

    //[SerializeField] string diceSideOneName;
    //[SerializeField] string diceSideTwoName;
    //[SerializeField] string diceSideThreeName;
    //[SerializeField] string diceSideFourName;
    //[SerializeField] string diceSideFiveName;
    //[SerializeField] string diceSideSixName;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(WaitUntilDiceStops());

    }

    // Update is called once per frame
    void Update()
    {
    }

    private void ThisSideIsDown()
    {
        if (diceSideOneIsDown == true)
            print("You rolled a 6");
        if (diceSideTwoIsDown == true)
            print("You rolled a 5");
        if (diceSideThreeIsDown == true)
            print("You rolled a 4");
        if (diceSideFourIsDown == true)
            print("You rolled a 3");
        if (diceSideFiveIsDown == true)
            print("You rolled a 2");
        if (diceSideSixIsDown == true)
            print("You rolled a 1");
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Dice")
        {
            print(col.gameObject.name);
            switch (col.gameObject.name)
            {
                case "DiceSideCollision (1)": //Side 6 is up
                    diceSideOneIsDown = true;
                    //diceSideTwoIsDown = false;
                    //diceSideThreeIsDown = false;
                    //diceSideFourIsDown = false;
                    //diceSideFiveIsDown = false;
                    //diceSideSixIsDown = false;
                    break;

                case "DiceSideCollision (2)": //Side 5 is up
                    diceSideTwoIsDown = true;
                    //diceSideOneIsDown = false;
                    //diceSideThreeIsDown = false;
                    //diceSideFourIsDown = false;
                    //diceSideFiveIsDown = false;
                    //diceSideSixIsDown = false;
                    break;

                case "DiceSideCollision (3)": //Side 4 is up
                    diceSideThreeIsDown = true;
                    //diceSideOneIsDown = false;
                    //diceSideTwoIsDown = false;
                    //diceSideFourIsDown = false;
                    //diceSideFiveIsDown = false;
                    //diceSideSixIsDown = false;
                    break;

                case "DiceSideCollision (4)": //Side 3 is up
                    diceSideFourIsDown = true;
                    //diceSideOneIsDown = false;
                    //diceSideTwoIsDown = false;
                    //diceSideThreeIsDown = false;
                    //diceSideFiveIsDown = false;
                    //diceSideSixIsDown = false;
                    break;

                case "DiceSideCollision (5)": //Side 2 is up
                    diceSideFiveIsDown = true;
                    //diceSideOneIsDown = false;
                    //diceSideTwoIsDown = false;
                    //diceSideThreeIsDown = false;
                    //diceSideFourIsDown = false;
                    //diceSideSixIsDown = false;
                    break;

                case "DiceSideCollision (6)": //Side 1 is up
                    diceSideSixIsDown = true;
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

    private IEnumerator WaitUntilDiceStops()
    {
        yield return new WaitForSeconds(5);
        ThisSideIsDown();
    }
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
