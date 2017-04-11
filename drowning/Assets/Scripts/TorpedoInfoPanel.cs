using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TorpedoInfoPanel : MonoBehaviour {

    [SerializeField]
    Text statusText, coordinateText;

    [SerializeField]
    LayoutElement layout;

    [SerializeField]
    GameObject content;

    float layoutInitialHeight;

    [SerializeField]
    float removeDuration = 0.5f;

    public void updateCoordinateText(Vector2 position)
    {
        coordinateText.text = "TOR X: " + Mathf.Floor(position.x) + " TOR Y: " + Mathf.Floor(position.y);
    }

    public void updateStatus(string status)
    {
        statusText.text = status;
    }

    public void Remove(float delay) //this makes the other list elements scroll smoothly into place, helps with continuity
    {
        StartCoroutine(RemoveInfoPanel(delay));
    }

    IEnumerator RemoveInfoPanel(float delay)
    {
        yield return new WaitForSeconds(delay);

        content.SetActive(false);

        layoutInitialHeight = layout.minHeight;

        float t = removeDuration;
        while(t > 0)
        {
            float newMinHeight = Mathf.Lerp(0, layoutInitialHeight, t / removeDuration);
            Debug.Log(newMinHeight);
            layout.minHeight = newMinHeight;

            yield return 0;
            t -= Time.deltaTime;
        }

        Destroy(this.gameObject);
    }
}
