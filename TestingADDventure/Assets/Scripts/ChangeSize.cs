using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChangeSize : MonoBehaviour
{
    public void Pulse()
    {
        transform.localScale = new Vector3(1, 1);
        StopCoroutine(ScaleDown());
        transform.localScale = new Vector3(transform.localScale.x * (1 + (GetComponent<Slider>().value * 0.01f)), transform.localScale.y * (1 + (GetComponent<Slider>().value * 0.01f)));
        StartCoroutine(ScaleDown());
    }

    IEnumerator ScaleDown()
    {
        for (float i = 0.3f; i > 0; i -= Time.deltaTime)
        {
            transform.localScale = new Vector3(transform.localScale.x - ((transform.localScale.x - 1) / 30), transform.localScale.y - ((transform.localScale.y - 1) / 30));
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
}
