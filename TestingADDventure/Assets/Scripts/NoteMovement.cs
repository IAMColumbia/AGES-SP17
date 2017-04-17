using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class NoteMovement : MonoBehaviour
{
    float ySpawnLocation = 500;
    float fallSpeed = -150;

    void Awake()
    {
        //transform.SetParent(GameObject.Find("RhythmGameBoardPanel").transform);
        transform.localPosition = new Vector3(transform.parent.GetComponent<PlayNoteSequence>().xPos, ySpawnLocation, 0);
    }

    void Update()
    {
        transform.Translate(new Vector2(0, fallSpeed) * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("enter");
        if (other.gameObject.name == "FailCollider")
        {
            Destroy(gameObject);
            Debug.Log("fail");
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        RaycastHit2D clickHit = Physics2D.Raycast(Input.mousePosition, Vector2.zero);

        if (Input.GetMouseButtonDown(0) && clickHit == other)
        {
            Debug.Log("click");
            bool noteHit = false;
            bool noteMiss = false;

            if (other.gameObject.tag == "HitCollider")
            {
                noteHit = true;
            }
            else if (other.gameObject.tag == "MissCollider")
            {
                noteMiss = true;
            }

            if ((noteHit && noteMiss) || noteHit)
            {
                Destroy(gameObject);
            }
            else if (noteMiss)
            {
                Debug.Log("miss");
            }
        }
    }
}
