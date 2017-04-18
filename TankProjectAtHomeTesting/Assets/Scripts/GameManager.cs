using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{
    [SerializeField]
    private int numberOfPlayers = 1;

    [SerializeField]
    private TankController tankPrefab;

    [SerializeField]
    private FootSoldierController footSoldierPrefab;

    [Tooltip("There must be as many start points as the max player count.")]
    [SerializeField]
    Transform[] tanksSartPoints;


    [Tooltip("There must be as many start points as the max player count.")]
    [SerializeField]
    Transform[] soldierStartPoints;

    // Use this for initialization
    void Start () 
	{
        for (int i = 0; i < numberOfPlayers; i++)
        {
            // Right now I'm creating the players here. Ultimately I'd probably create them on some
            // player join screen that happens before a match starts.
            Player player = new Player(i+1);
            TankController tank = Instantiate(tankPrefab, tanksSartPoints[i].position, tanksSartPoints[i].rotation) as TankController;

            FootSoldierController soldier = Instantiate(footSoldierPrefab, soldierStartPoints[i].position, soldierStartPoints[i].rotation) as FootSoldierController;
            soldier.Initialize(player);
            // Want to spawn soldiers that get into tanks now.
            // what if it was like hot potatoes and not everyone gets a tank?
            //tank.ControllingPlayer = player;
        }
	}
	

}
