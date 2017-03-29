using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class annoyingFace : MonoBehaviour {

    [SerializeField]
    Sprite[] faces;

    [SerializeField]
    float flipTime, switchTime;

    Image img;

    int spriteIndex = 0;

	// Use this for initialization
	void Start () {
        img = GetComponent<Image>();


	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void OnEnable()
    {
        StartCoroutine(flipAfter(flipTime));
        StartCoroutine(switchAfter(switchTime));
    }

    IEnumerator flipAfter(float time)
    {
        yield return new WaitForSeconds(time);
        img.transform.Rotate(new Vector3(0, 180, 0));
        StartCoroutine(flipAfter(time));
    }

    IEnumerator switchAfter(float time)
    {
        yield return new WaitForSeconds(time);
        spriteIndex++;
        if(spriteIndex == faces.Length)
        {
            spriteIndex = 0;
        }

        img.sprite = faces[spriteIndex];

        StartCoroutine(switchAfter(time));
    }
}
