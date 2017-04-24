using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IActivatable
{
    //Do whatever the object is supposed to do when triggered

    //IF YOU WANT TO ADD A NEW ITEM FROM HERE ON OUT...
    //All you have to do is add a script for each item, implement Activate, and call it a day
    //Oh, and add a GUI button
    void Activate(GameObject ObjectActivatedBy);
}
