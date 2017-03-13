using UnityEngine;
using System.Collections;

public abstract class Command 
{
    abstract public void Execute(CharacterController character);
    abstract public void Undo(CharacterController character);
}
