using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{
    [SerializeField]
    private int numberOfPlayers = 1;

    [SerializeField]
    private TankController tankPrefab;

    [Tooltip("There must be as many start points as the max player count.")]
    [SerializeField]
    Transform[] startPoints;

	// Use this for initialization
	void Start () 
	{
        for (int i = 1; i <= numberOfPlayers; i++)
        {
            Player player = new Player(i);
            TankController tank = Instantiate(tankPrefab, startPoints[i].position, startPoints[i].rotation) as TankController;
            tank.ControllingPlayer = player;
        }
	}
	

}
