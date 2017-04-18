using UnityEngine;
using System.Collections;

public class EnterAndExitTanks : MonoBehaviour 
{
    [SerializeField]
    FootSoldierController controller;

    [SerializeField]
    float exitVelocity = 10;

    private float enterExitCooldown = 1f;
    private bool isOnCooldown = false;
    private string EnterExitTankButton = "EnterExitTankP";


    // Have to make sure this gets called after the player gets created
    public void Initialize()
    {
        EnterExitTankButton += controller.ControllingPlayer.PlayerNumber;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!controller.IsInTank && !isOnCooldown)
        {
            TankController tank = other.GetComponentInParent<TankController>();

            if (tank != null && tank.ControllingSoldier == null && Input.GetButtonDown(EnterExitTankButton))
            {
                isOnCooldown = true;
                controller.EnterTank(tank);
                StartCoroutine(HandleCooldown());
            }
        }
    }

    private void Update()
    {
        if (controller.IsInTank && !isOnCooldown)
        {
            if (Input.GetButtonDown(EnterExitTankButton))
            {
                Debug.Log("Exit Tank!");
                isOnCooldown = true;
                controller.ExitTank(exitVelocity);
                StartCoroutine(HandleCooldown());
            }
        }
    }

    private IEnumerator HandleCooldown()
    {
        yield return new WaitForSeconds(enterExitCooldown);

        isOnCooldown = false;
    }
}
