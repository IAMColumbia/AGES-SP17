using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DisplayWinLose : MonoBehaviour {

    [SerializeField]
    Sprite winSprite, loseSprite;

    [SerializeField]
    float duration;

    [SerializeField]
    AnimationCurve curve;

    [SerializeField]
    Image winLoseImage;

    [SerializeField]
    Image bg;

    Color bgColor;

	// Use this for initialization
	void Start () {
        winLoseImage.rectTransform.anchorMin = new Vector2(1, 0);
        bgColor = bg.color;
        bg.color = new Color(bgColor.r, bgColor.g, bgColor.b, 0);
	}

    public void PlayerWon(bool isWinner)
    {
        StartCoroutine(DisplayWinLoss(isWinner));
    }

    IEnumerator DisplayWinLoss(bool winner)
    {
        if (winner)
        {
            winLoseImage.sprite = winSprite;
        }
        else
        {
            winLoseImage.sprite = loseSprite;
        }

        float t = 0;

        while (t < duration)
        {
            t += Time.deltaTime;

            float position = Mathf.Lerp(0, 1, curve.Evaluate(t / duration));

            winLoseImage.rectTransform.anchorMin = new Vector2(1 - position, 0);
            bg.color = new Color(bgColor.r, bgColor.b, bgColor.g, position * bgColor.a);

            yield return 0;
        }

        winLoseImage.rectTransform.anchorMin = new Vector2(0, 0);
        bg.color = new Color(bgColor.r, bgColor.b, bgColor.g, bgColor.a);

    }
}
