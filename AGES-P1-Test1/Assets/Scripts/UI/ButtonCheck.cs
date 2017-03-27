using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonCheck : MonoBehaviour
{
    [SerializeField]
    string prefix;

    [SerializeField]
    string buttonToTest;

    [SerializeField]
    public bool checkedInput;

    [SerializeField]
    Image image;

    [SerializeField]
    bool isAxis;

    [SerializeField]
    bool isButton;

	// Use this for initialization
	void Start ()
    {

        image = GetComponentInParent<Image>();
	
	}

    // Update is called once per frame
    void Update()
    {
        if (isButton)
        {
            checkingButton();
        }

        if (isAxis)
        {
            checkingAxis();
        }
    }

    private void checkingAxis()
    {
        if (Input.GetAxis(prefix + "_" + buttonToTest) != 0)
        {
            if (prefix == "P1")
            {
                image.color = Color.red;
            }

            if (prefix == "P2")
            {
                image.color = Color.blue;
            }

            checkedInput = true;
        }
    }

    private void checkingButton()
{
    if (Input.GetButton(prefix + "_" + buttonToTest))
    {
        if (prefix == "P1")
        {
            image.color = Color.red;
        }

        if (prefix == "P2")
        {
            image.color = Color.blue;
        }

        checkedInput = true;
    }
}
}