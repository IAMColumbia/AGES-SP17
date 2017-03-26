using UnityEngine;
using System.Collections;


public class CheckpointAndFinishLine : MonoBehaviour
{
    [SerializeField]
    private int checkPointNumber = 1;
    [SerializeField]
    private bool isFinishLine = false;
    [SerializeField]
    private TankLapProgressTracker player1Progress;
    [SerializeField]
    private TankLapProgressTracker player2Progress;
    [SerializeField]
    private TankLapProgressTracker player3Progress;
    [SerializeField]
    private TankLapProgressTracker player4Progress;

    void OnTriggerEnter(Collider other)
    {
        CheckPlayer1(other);
        CheckPlayer2(other);
        CheckPlayer3(other);
        CheckPlayer4(other);
    }

    private void CheckPlayer1(Collider other)
    {
        if (other == GameObject.FindGameObjectWithTag("Player").GetComponent<Collider>())
        {
            if (checkPointNumber == 1)
            {
                player1Progress.hasPassedCheckpoint1 = true;

            }
            else if (checkPointNumber == 2 && player1Progress.hasPassedCheckpoint1)
            {
                player1Progress.hasPassedCheckpoint2 = true;
            }
            else if (checkPointNumber == 3 && player1Progress.hasPassedCheckpoint1 && player1Progress.hasPassedCheckpoint2)
            {
                player1Progress.hasPassedCheckpoint3 = true;
            }
            else if (checkPointNumber == 4 && player1Progress.hasPassedCheckpoint1 && player1Progress.hasPassedCheckpoint2 && player1Progress.hasPassedCheckpoint3)
            {
                player1Progress.hasPassedCheckpoint4 = true;
            }
            else if (isFinishLine && player1Progress.hasPassedCheckpoint1 && player1Progress.hasPassedCheckpoint2 && player1Progress.hasPassedCheckpoint3 && player1Progress.hasPassedCheckpoint4)
            {
                player1Progress.currentLap++;
                player1Progress.hasPassedCheckpoint1 = false;
                player1Progress.hasPassedCheckpoint2 = false;
                player1Progress.hasPassedCheckpoint3 = false;
                player1Progress.hasPassedCheckpoint4 = false;
            }
            else
            {
                player1Progress.hasPassedCheckpoint1 = false;
                player1Progress.hasPassedCheckpoint2 = false;
                player1Progress.hasPassedCheckpoint3 = false;
                player1Progress.hasPassedCheckpoint4 = false;
            }
        }
    }

    private void CheckPlayer2(Collider other)
    {
        if (other == GameObject.FindGameObjectWithTag("Player2").GetComponent<Collider>())
        {
            if (checkPointNumber == 1)
            {
                player2Progress.hasPassedCheckpoint1 = true;

            }
            else if (checkPointNumber == 2 && player2Progress.hasPassedCheckpoint1)
            {
                player2Progress.hasPassedCheckpoint2 = true;
            }
            else if (checkPointNumber == 3 && player2Progress.hasPassedCheckpoint1 && player2Progress.hasPassedCheckpoint2)
            {
                player2Progress.hasPassedCheckpoint3 = true;
            }
            else if (checkPointNumber == 4 && player2Progress.hasPassedCheckpoint1 && player2Progress.hasPassedCheckpoint2 && player2Progress.hasPassedCheckpoint3)
            {
                player2Progress.hasPassedCheckpoint4 = true;
            }
            else if (isFinishLine && player2Progress.hasPassedCheckpoint1 && player2Progress.hasPassedCheckpoint2 && player2Progress.hasPassedCheckpoint3 && player2Progress.hasPassedCheckpoint4)
            {
                player2Progress.currentLap++;
                player2Progress.hasPassedCheckpoint1 = false;
                player2Progress.hasPassedCheckpoint2 = false;
                player2Progress.hasPassedCheckpoint3 = false;
                player2Progress.hasPassedCheckpoint4 = false;
            }
            else
            {
                player2Progress.hasPassedCheckpoint1 = false;
                player2Progress.hasPassedCheckpoint2 = false;
                player2Progress.hasPassedCheckpoint3 = false;
                player2Progress.hasPassedCheckpoint4 = false;
            }
        }
    }

    private void CheckPlayer3(Collider other)
    {
        if (other == GameObject.FindGameObjectWithTag("Player3").GetComponent<Collider>())
        {
            if (checkPointNumber == 1)
            {
                player3Progress.hasPassedCheckpoint1 = true;

            }
            else if (checkPointNumber == 2 && player3Progress.hasPassedCheckpoint1)
            {
                player3Progress.hasPassedCheckpoint2 = true;
            }
            else if (checkPointNumber == 3 && player3Progress.hasPassedCheckpoint1 && player3Progress.hasPassedCheckpoint2)
            {
                player3Progress.hasPassedCheckpoint3 = true;
            }
            else if (checkPointNumber == 4 && player3Progress.hasPassedCheckpoint1 && player3Progress.hasPassedCheckpoint2 && player3Progress.hasPassedCheckpoint3)
            {
                player3Progress.hasPassedCheckpoint4 = true;
            }
            else if (isFinishLine && player3Progress.hasPassedCheckpoint1 && player3Progress.hasPassedCheckpoint2 && player3Progress.hasPassedCheckpoint3 && player3Progress.hasPassedCheckpoint4)
            {
                player3Progress.currentLap++;
                player3Progress.hasPassedCheckpoint1 = false;
                player3Progress.hasPassedCheckpoint2 = false;
                player3Progress.hasPassedCheckpoint3 = false;
                player3Progress.hasPassedCheckpoint4 = false;
            }
            else
            {
                player3Progress.hasPassedCheckpoint1 = false;
                player3Progress.hasPassedCheckpoint2 = false;
                player3Progress.hasPassedCheckpoint3 = false;
                player3Progress.hasPassedCheckpoint4 = false;
            }
        }
    }

    private void CheckPlayer4(Collider other)
    {
        if (other == GameObject.FindGameObjectWithTag("Player4").GetComponent<Collider>())
        {
            if (checkPointNumber == 1)
            {
                player4Progress.hasPassedCheckpoint1 = true;

            }
            else if (checkPointNumber == 2 && player4Progress.hasPassedCheckpoint1)
            {
                player4Progress.hasPassedCheckpoint2 = true;
            }
            else if (checkPointNumber == 3 && player4Progress.hasPassedCheckpoint1 && player4Progress.hasPassedCheckpoint2)
            {
                player4Progress.hasPassedCheckpoint3 = true;
            }
            else if (checkPointNumber == 4 && player4Progress.hasPassedCheckpoint1 && player4Progress.hasPassedCheckpoint2 && player4Progress.hasPassedCheckpoint3)
            {
                player4Progress.hasPassedCheckpoint4 = true;
            }
            else if (isFinishLine && player4Progress.hasPassedCheckpoint1 && player4Progress.hasPassedCheckpoint2 && player4Progress.hasPassedCheckpoint3 && player4Progress.hasPassedCheckpoint4)
            {
                player4Progress.currentLap++;
                player4Progress.hasPassedCheckpoint1 = false;
                player4Progress.hasPassedCheckpoint2 = false;
                player4Progress.hasPassedCheckpoint3 = false;
                player4Progress.hasPassedCheckpoint4 = false;
            }
            else
            {
                player4Progress.hasPassedCheckpoint1 = false;
                player4Progress.hasPassedCheckpoint2 = false;
                player4Progress.hasPassedCheckpoint3 = false;
                player4Progress.hasPassedCheckpoint4 = false;
            }
        }
    }
}
