using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChangeSize : MonoBehaviour
{
    [SerializeField]
    Canvas canvas;

    public void Pulse()
    {
        RectTransform rect = canvas.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(100, 100);
        //rect.sizeDelta = new Vector2(/*rect.sizeDelta.x * */GetComponent<Slider>().value, /*rect.sizeDelta.y * */GetComponent<Slider>().value);
        Debug.Log("pulse");
        transform.localScale = new Vector3(transform.localScale.x * (1 + (GetComponent<Slider>().value * 0.01f)), transform.localScale.y * (1 + (GetComponent<Slider>().value * 0.01f)));
        StartCoroutine(ScaleDown());
    }

    IEnumerator ScaleDown()
    {
        yield return new WaitForSeconds(0.3f);
        transform.localScale = new Vector3(1, 1);
    }
}
