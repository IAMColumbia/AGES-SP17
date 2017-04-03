using UnityEngine;
using System.Collections;

public class DropRingAfterTime : MonoBehaviour
{

    [SerializeField]
    int secondsBeforeRingDrops;
    [SerializeField]
    Rigidbody rigidBody;
    [SerializeField]
    GameObject disableThis;
    [SerializeField]
    Transform spawnPoint;

    float secondsBeforeDisableRing = 3;

	void Start ()
    {
        StartCoroutine(WaitThenDropRing());
	}

    IEnumerator WaitThenDropRing()
    {
        yield return new WaitForSeconds(secondsBeforeRingDrops);

        rigidBody.isKinematic = false;

        StartCoroutine(WaitThenDisableRing());
        //Destroy(disableThis, secondsBeforeDisableRing);
    }

    IEnumerator WaitThenDisableRing()
    {
        yield return new WaitForSeconds(secondsBeforeDisableRing);

        rigidBody.isKinematic = true;
        disableThis.SetActive(false);
    }

    public void Reset()
    {
        transform.position = spawnPoint.position;
        rigidBody.isKinematic = true;
        disableThis.SetActive(true);

        StopAllCoroutines();
        StartCoroutine(WaitThenDropRing());
    }
}
