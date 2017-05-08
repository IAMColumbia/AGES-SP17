using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinParticleEffects : MonoBehaviour
{
    GameObject particleGameObject;
    int delayLength = 3;

    private void Start()
    {
        particleGameObject = GetComponentInChildren<ParticleSystem>().gameObject;
        particleGameObject.SetActive(false);
    }

    public void ActivateWinEffect()
    {
        particleGameObject.SetActive(true);
        StartCoroutine(DeactivateSelf());
    }

    IEnumerator DeactivateSelf()
    {
        yield return new WaitForSeconds(particleGameObject.GetComponent<AudioSource>().clip.length * delayLength);
        particleGameObject.SetActive(false);
    }
}
