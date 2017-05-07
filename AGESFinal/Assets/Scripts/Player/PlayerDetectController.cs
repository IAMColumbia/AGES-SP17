using UnityEngine;
using System.Collections;

public class PlayerDetectController : MonoBehaviour {


    [SerializeField]
    private GameObject[] Players;

    private enum PlayerAmount { OnePlayer, TwoPlayer, ThreePlayer, FourPlayer }
    private PlayerAmount currentPlayerAmount;

	void Start () {

        for (int i = 0; i < Players.Length; i++)
        {
            Players[i].SetActive(false);
        }

	}
	
	// Update is called once per frame
	void Update () {
        
        switch(currentPlayerAmount)
        {
            case PlayerAmount.OnePlayer:

                for (int i = 0; i < Input.GetJoystickNames().Length; i++)
                {
                    Players[i].SetActive(true);

                    if (Input.GetJoystickNames().Length > 1)
                        currentPlayerAmount = PlayerAmount.TwoPlayer;
                }
                break;

            case PlayerAmount.TwoPlayer:

                for (int i = 0; i < Input.GetJoystickNames().Length; i++)
                {
                    Players[i].SetActive(true);

                    if (Input.GetJoystickNames().Length > 2)
                        currentPlayerAmount = PlayerAmount.TwoPlayer;
                }
                break;

            case PlayerAmount.ThreePlayer:

                for (int i = 0; i < Input.GetJoystickNames().Length; i++)
                {
                    Players[i].SetActive(true);

                    if (Input.GetJoystickNames().Length > 3)
                        currentPlayerAmount = PlayerAmount.TwoPlayer;
                }
                break;

            case PlayerAmount.FourPlayer:

                for (int i = 0; i < Input.GetJoystickNames().Length; i++)
                {
                    Players[i].SetActive(true);
                }
                break;

        }
	}
}
