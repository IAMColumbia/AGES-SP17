using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;
using System;

public class GoalSphere : MonoBehaviour
    {

        // Use this for initialization
        bool shouldDisableWhenDonePlayingSoundEffect = false;
        public AudioSource audioSource;
        float duration = 3;

        [SerializeField]
        GameObject goalSphereToggle;
    GameManager gameManager;
    [SerializeField]
    GameObject centerPlatform;

    [SerializeField]
    Vector3 winnerPlatform; 
    PlayerManager[] m_Tanks;
  //(center.transform.position.x, center.transform.position.y, transform.position.z);

    
    void Start()
    {
       // AudioSource audioSource = GetComponent<AudioSource>();
       goalSphereToggle.SetActive(false);
       
    }
     void Update()
    {
        Rotation();
    }

    void Rotation()
    {
        transform.Rotate(0, 100 * Time.deltaTime, 0);
        if (shouldDisableWhenDonePlayingSoundEffect)
        {
            transform.Rotate(25, 300 * Time.deltaTime, 100);
            FadeOut();
        }
    }

    void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.tag == "Player")
            {
              Debug.Log("Player touched Sphere");
              //audioSource.Play();
              goalSphereToggle.SetActive(true);
              centerPlatform.SetActive(true);
              other.gameObject.transform.position = winnerPlatform;                    
            }
        }
    public void FadeOut()
        {
            if (shouldDisableWhenDonePlayingSoundEffect && !audioSource.isPlaying)
            {
                float t;
                float alpha = GetComponent<MeshRenderer>().material.color.a;
                for (t = 0.0f; t < 1.0f; t += Time.deltaTime / duration)
                {
                    Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, 0, t));
                    GetComponent<MeshRenderer>().material.color = newColor;
                }
            }
        }
}
