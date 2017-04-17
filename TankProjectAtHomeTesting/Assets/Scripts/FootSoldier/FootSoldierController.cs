using UnityEngine;
using System.Collections;
using System;

public class FootSoldierController : MonoBehaviour 
{
    #region Properties
    public Player ControllingPlayer { get; private set; }
    public bool IsInTank { get; private set; }
    #endregion


    #region Monobehaviour functions
    private void Start()
    {
        IsInTank = false;
        // TODO: this needs to be real and not hardcoded to P1
        ControllingPlayer = new Player(1);
    }
    #endregion

    private void EnterTank(TankController parentTank)
    {        
        IsInTank = true;
        transform.SetParent(parentTank.transform);
    }
}
