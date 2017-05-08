using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IActivatable
{
    //Do whatever the object is supposed to do when triggered
    void Activate(GameObject ObjectActivatedBy);
}
