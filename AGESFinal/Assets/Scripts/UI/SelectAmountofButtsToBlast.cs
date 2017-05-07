using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class SelectAmountofButtsToBlast : MonoBehaviour {

    public Text text;
    public static int amount;

    private bool axisInUse = false;
    private GameManager gameManager;

    private void Start()
    {
        text = GetComponent<Text>();
        amount = 0;
    }
    
    private void Update()
    {
        Debug.Log(Input.GetAxis("Horizontal1"));
        if (Input.GetAxis("Horizontal1") > 0.5f && amount != 15)
        {
            if(!axisInUse)
            {
                amount++;
                text.text = amount.ToString();
                axisInUse = true;
            }
        }
        else if (Input.GetAxis("Horizontal") <  -0.5f && amount != 0)
        {
            if (!axisInUse)
            {
                amount--;
                text.text = amount.ToString();
                axisInUse = true;
            }
        }
        else
        {
            axisInUse = false;
        }
        gameManager.buttsToBlast = amount;
    }
}
