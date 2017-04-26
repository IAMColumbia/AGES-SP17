using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour
{
    [SerializeField]
    Sprite damageSprite;
    [SerializeField]
    int hp = 4;

    [SerializeField]
    AudioClip chopSound1;
    [SerializeField]
    AudioClip chopSound2;

    SpriteRenderer spriteRenderer;

	void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
	}

    public void DamageWall(int loss)
    {
        spriteRenderer.sprite = damageSprite;

        SoundManager.instance.RandomizeSFX(chopSound1, chopSound2);

        hp -= loss;

        if (hp <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
