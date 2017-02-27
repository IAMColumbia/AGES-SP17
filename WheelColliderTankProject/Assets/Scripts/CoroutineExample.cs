using UnityEngine;
using System.Collections;

public class CoroutineExample : MonoBehaviour
{
    void OnMouseDown()
    {
        StartCoroutine(WaitBeforeIncrementingCoins());
    }

    private IEnumerator WaitBeforeIncrementingCoins()
    {
        yield return new WaitForSeconds(2);
    }
}
