using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource backgroundMusic;
    [SerializeField] AudioSource soundEffects;

    [SerializeField] AudioClip LevelEditorTheme;
    [SerializeField] AudioClip InGameTheme;
    [SerializeField] AudioClip MenuTheme;

    #region SFX

    [SerializeField] AudioClip clickSFX;

    #endregion

    static AudioManager reference = null;

    private void Start()
    {
        if (reference == null)
        {
            DontDestroyOnLoad(this.gameObject);
            reference = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void PlayLevelEditorTheme()
    {
        if (backgroundMusic.clip != LevelEditorTheme)
        {
            backgroundMusic.clip = LevelEditorTheme;
            backgroundMusic.Play();
        }
    }

    public void PlayInGameTheme()
    {
        if (backgroundMusic.clip != InGameTheme)
        {
            backgroundMusic.clip = InGameTheme;
            backgroundMusic.Play();
        }
    }

    public void PlayMenuTheme()
    {
        if (backgroundMusic.clip != MenuTheme)
        {
            backgroundMusic.clip = MenuTheme;
            backgroundMusic.Play();
        }
    }

    public void playSFX(AudioClip desiredSFX)
    {
        soundEffects.clip = desiredSFX;
        soundEffects.Play();
    }
}
