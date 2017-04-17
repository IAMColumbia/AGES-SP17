using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayNoteSequence : MonoBehaviour
{
    public List<float> whichNote = new List<float>() { 1, 2, 3 };
    int noteMark = 0;
    bool timerReset = true;
    Transform noteObject;

    [HideInInspector]
    public float xPos;

    [SerializeField]
    Transform notePrefab;
    [SerializeField]
    Color note1Color;
    [SerializeField]
    Color note2Color;
    [SerializeField]
    Color note3Color;

    void Update()
    {
        if (timerReset == true)
        {
            StartCoroutine(SpawnNote());
            timerReset = false;
        }
    }

    IEnumerator SpawnNote()
    {
        yield return new WaitForSeconds(1);

        if (whichNote[noteMark] == 1)
        {
            xPos = -195f;
            noteObject = notePrefab;
            noteObject.GetComponent<Image>().color = note1Color;
        }
        if (whichNote[noteMark] == 2)
        {
            xPos = 0f;
            noteObject = notePrefab;
            noteObject.GetComponent<Image>().color = note2Color;
        }
        if (whichNote[noteMark] == 3)
        {
            xPos = 195f;
            noteObject = notePrefab;
            noteObject.GetComponent<Image>().color = note3Color;
        }

        noteMark++;
        timerReset = true;
        Vector3 notePosition = new Vector3(xPos, 400f, 0);
        GameObject newNote = Instantiate(noteObject, transform, false) as GameObject;

        if (noteMark >= whichNote.Count)
        {
            noteMark = 0;
        }
    }
}
