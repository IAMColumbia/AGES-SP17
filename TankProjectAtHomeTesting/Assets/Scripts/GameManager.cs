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
        for (int i = 0; i < numberOfPlayers; i++)
        {
            // Right now I'm creating the players here. Ultimately I'd probably create them on some
            // player join screen that happens before a match starts.
            Player player = new Player(i+1);
            TankController tank = Instantiate(tankPrefab, startPoints[i].position, startPoints[i].rotation) as TankController;
            tank.ControllingPlayer = player;
        }
	}
	

}
