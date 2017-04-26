using UnityEngine;
using System.Collections.Generic;

public class InputHandler : MonoBehaviour
{
    public enum ButtonState { Off, Press, Hold, Release };
    public enum ControlScheme { KeyboardMouse, Gamepad1, Gamepad2, Gamepad3, Gamepad4 };

    public class Button
    {
        public bool isDown, wasDown;

        public ButtonState state;

        public string axis;

        public Button(string axis)
        {
            isDown = false;
            wasDown = false;
            this.axis = axis;
            state = ButtonState.Off;
        }
    }

    public ControlScheme controlScheme;
    public string inputID;

    Vector4 axes;

    public float horzAxisLeft
    {
        get
        {
            return axes.x;
        }
    }
    public float vertAxisLeft
    {
        get
        {
            return axes.y;
        }
    }
    public float horzAxisRight
    {
        get
        {
            return axes.z;
        }
    }
    public float vertAxisRight
    {
        get
        {
            return axes.w;
        }
    }
    public Vector3 mousePos;
    public static List<List<Button>> buttonLists = new List<List<Button>>();
    public List<Button> buttons;
    

    void Start()
    {
        axes = Vector4.zero;
        mousePos = Input.mousePosition;

        SetButtons();
    }

    void Update()
    {
        if (controlScheme == ControlScheme.KeyboardMouse)
        {
                mousePos = Input.mousePosition;
        }

        GetAxes();
        
        foreach (Button b in buttons)
        {
            b.isDown = Input.GetAxis(b.axis) != 0;
            SetButtonState(b);
            b.wasDown = b.isDown;
        }
    }

    private void GetAxes()
    {
        if (controlScheme == ControlScheme.KeyboardMouse)
        {
            axes.x = Input.GetAxisRaw("Horizontal");
            axes.y = Input.GetAxisRaw("Vertical");
        }
        else
        {
            axes.x = Input.GetAxisRaw("Joy" + (int)controlScheme + "Horz");
            axes.y = Input.GetAxisRaw("Joy" + (int)controlScheme + "Vert");
            axes.z = Input.GetAxisRaw("Joy" + (int)controlScheme + "RightHorz");
            axes.w = Input.GetAxisRaw("Joy" + (int)controlScheme + "RightVert");
        }
    }

    public void SetButtons()
    {
        buttons = new List<Button>();
        inputID = "";
        if (controlScheme != ControlScheme.KeyboardMouse)
        {
            inputID = "Joy" + (int)controlScheme;
        }
        buttons.Add(new Button(inputID + "Dash"));
        buttons.Add(new Button(inputID + "Projectile"));
        buttons.Add(new Button(inputID + "Melee"));
        buttons.Add(new Button(inputID + "Sit"));
        buttons.Add(new Button(inputID + "Submit"));
        buttons.Add(new Button(inputID + "Heal"));
        buttons.Add(new Button(inputID + "Use"));
        buttonLists.Add(buttons);
    }

    public void ResetInput()
    {
        axes = Vector4.zero;
    }

    private static void SetButtonState(Button b)
    {
        if (b.wasDown)
        {
            if (b.isDown)
            {
                b.state = ButtonState.Hold;
            }
            else
            {
                b.state = ButtonState.Release;
            }
        }
        else
        {
            if (b.isDown)
            {
                b.state = ButtonState.Press;
            }
            else
            {
                b.state = ButtonState.Off;
            }
        }
    }

    
}