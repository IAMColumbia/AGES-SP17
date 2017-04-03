using UnityEngine;
using System.Collections;

public class DropRingAfterTime : MonoBehaviour
{

    [SerializeField]
    int secondsBeforeRingDrops;
    [SerializeField]
    Rigidbody rigidBody;
    [SerializeField]
    GameObject destroyThis;

    float secondsBeforeDestroyRing = 5;

	void Start ()
    {
        StartCoroutine(WaitThenDropRing());
	}

    IEnumerator WaitThenDropRing()
    {
        yield return new WaitForSeconds(secondsBeforeRingDrops);

        rigidBody.isKinematic = false;

        Destroy(destroyThis, secondsBeforeDestroyRing);
    }
}
