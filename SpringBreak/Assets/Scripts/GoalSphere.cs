using UnityEngine;
using System.Collections;

public class GoalSphere : MonoBehaviour {

    // Use this for initialization
    bool shouldDisableWhenDonePlayingSoundEffect = false;
    AudioSource audioSource;
    float duration = 3;
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
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
            gameObject.SetActive(false);

        }
    }
}
