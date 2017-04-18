using UnityEngine;
using System.Collections;
using System;

public class FootSoldierController : MonoBehaviour 
{
    [SerializeField]
    private GameObject soldierGraphics;

    [SerializeField]
    private new Rigidbody rigidbody;

    private TankController tankBeingDriven = null;

    #region Properties
    public Player ControllingPlayer { get; private set; }
    public bool IsInTank { get { return tankBeingDriven != null; } }
    #endregion

    public void Initialize(Player player)
    {
        ControllingPlayer = player;
        GetComponent<EnterAndExitTanks>().Initialize();
        GetComponent<ColorModelFromPlayerNumber>().ApplyColor(player.PlayerNumber);
    }


    public void EnterTank(TankController tankToEnter)
    {                
        soldierGraphics.SetActive(false);
        tankBeingDriven = tankToEnter;
        tankBeingDriven.ControllingSoldier = this;
    }

    public void ExitTank(float exitVelocity)
    {
        // get a position above the tank and set the player there
        float distanceAboveTank = 5;
        transform.position = tankBeingDriven.transform.position + Vector3.up * distanceAboveTank;

        soldierGraphics.SetActive(true);


        // Add some velocity so it looks like they are popping up
        rigidbody.velocity = Vector3.up * exitVelocity;

        tankBeingDriven.ControllingSoldier = null;
        tankBeingDriven = null;
    }
}
