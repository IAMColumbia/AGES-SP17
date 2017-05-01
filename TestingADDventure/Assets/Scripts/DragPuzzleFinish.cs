using UnityEngine;
using System.Collections;

public class DragPuzzleFinish : MonoBehaviour
{
    [SerializeField]
    GameObject tile1;
    [SerializeField]
    GameObject tile2;
    [SerializeField]
    GameObject tile3;
    [SerializeField]
    GameObject tile4;
    [SerializeField]
    Transform slot1;
    [SerializeField]
    Transform slot2;
    [SerializeField]
    Transform slot3;
    [SerializeField]
    Transform slot4;
    [SerializeField]
    GameObject dragPuzzleBackground;
    [SerializeField]
    DistractionManager distractionManager;

    Transform tile1CorrectSlot;
    Transform tile2CorrectSlot;
    Transform tile3CorrectSlot;
    Transform tile4CorrectSlot;
    GameObject[] tiles = new GameObject[4];
    Transform[] correctSlots = new Transform[4];

    void Start()
    {
        tile1CorrectSlot = slot4;
        tile2CorrectSlot = slot2;
        tile3CorrectSlot = slot1;
        tile4CorrectSlot = slot3;

        correctSlots[0] = tile1CorrectSlot;
        correctSlots[1] = tile2CorrectSlot;
        correctSlots[2] = tile3CorrectSlot;
        correctSlots[3] = tile4CorrectSlot;

        tiles[0] = tile1;
        tiles[1] = tile2;
        tiles[2] = tile3;
        tiles[3] = tile4;
    }

    public void CheckAnswer()
    {
        int score = 0;

        for (int i = 0; i < tiles.Length; i++)
        {
            if (tiles[i].transform.parent == correctSlots[i])
                score++;
        }

        if (score == 4)
        {
            distractionManager.draggingPuzzleScore = score;
        }
        else
        {
            //play incorrect answer sound, buzzer
            Debug.Log("lose");
        }
    }
}
