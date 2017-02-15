using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class PlayerCharacter : MonoBehaviour 
{
    CharacterController characterToControl;
    Command lastCommand;

    void Start()
    {
        characterToControl = GetComponent<CharacterController>();
    }

	// Update is called once per frame
	void Update () 
	{
	    if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveCommand moveCommand = new MoveCommand(0, 1);
            moveCommand.Execute(characterToControl);
            lastCommand = moveCommand;
        }

        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveCommand moveCommand = new MoveCommand(0, -1);
            moveCommand.Execute(characterToControl);
            lastCommand = moveCommand;
        }

        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveCommand moveCommand = new MoveCommand(-1, 0);
            moveCommand.Execute(characterToControl);
            lastCommand = moveCommand;
        }

        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveCommand moveCommand = new MoveCommand(1, 0);
            moveCommand.Execute(characterToControl);
            lastCommand = moveCommand;
        }

        else if (Input.GetButtonDown("Cancel"))
        {
            lastCommand.Undo(characterToControl);
        }
	}
}
