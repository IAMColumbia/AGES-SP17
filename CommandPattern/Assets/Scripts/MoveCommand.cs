using UnityEngine;
using System.Collections;
using System;

public class MoveCommand : Command
{
    float x;
    float y;

    Vector3 previousPosition;

    public override void Execute(CharacterController character)
    {
        previousPosition = character.transform.position;
        character.Move(x, y);
    }

    public override void Undo(CharacterController character)
    {
        character.transform.position = previousPosition;
    }

    public MoveCommand(float x,float y)
    {
        this.x = x;
        this.y = y;
    }
}
