using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class NoteMovement : MonoBehaviour
{
    DistractionManager distractionManager;
    float ySpawnLocation = 500;
    float fallSpeed = -150;

    void Awake()
    {
        distractionManager = GameObject.Find("DistractionManager").GetComponent<DistractionManager>();
        transform.localPosition = new Vector3(transform.parent.GetComponent<PlayNoteSequence>().xPos, ySpawnLocation, 0);
    }

    void Update()
    {
        transform.Translate(new Vector2(0, fallSpeed) * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "FailCollider")
        {
            ParticleSystem noteHitParticle = transform.GetChild(0).GetComponent<ParticleSystem>();
            noteHitParticle.Play();
            noteHitParticle.transform.parent = null;
            Destroy(gameObject);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        RaycastHit2D clickHit = Physics2D.Raycast(Input.mousePosition, Vector2.zero);

        if (Input.GetMouseButtonDown(0) && clickHit == other)
        {
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
                distractionManager.rhythmGameScore++;
                Destroy(gameObject);
            }
            else if (noteMiss)
            {
                Debug.Log("miss");
            }
        }
    }
}
