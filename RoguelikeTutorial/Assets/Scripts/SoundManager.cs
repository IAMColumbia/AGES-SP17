using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    float lowPitchRange = .95f;
    [SerializeField]
    float highPitchRange = 1.05f;

    [SerializeField]
    AudioSource fxSource;

    public AudioSource musicSource;

    public static SoundManager instance = null;

	void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
	}
	
	public void PlaySingle(AudioClip clip)
    {
        fxSource.clip = clip;
        fxSource.Play();
    }

    public void RandomizeSFX(params AudioClip[] clips)
    {
        int randomIndex = Random.Range(0, clips.Length);
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);

        fxSource.pitch = randomPitch;
        fxSource.clip = clips[randomIndex];
        fxSource.Play();
    }
}
