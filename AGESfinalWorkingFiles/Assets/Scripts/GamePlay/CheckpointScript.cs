using UnityEngine;
using System.Collections;

public class CheckpointScript : MonoBehaviour
{
    [SerializeField]
    Material[] materials;

    Renderer renderer;

    float rotationSpeed;

    float checkpointActivationSpeed;

    // Use this for initialization
    void Start()
    {
        rotationSpeed = 25f;
        renderer = GetComponent<Renderer>();
        renderer.material = materials[0];
        checkpointActivationSpeed = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(1f * rotationSpeed * Time.deltaTime, 1f * rotationSpeed * Time.deltaTime, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(ActivateCheckPoint());
        }
    }

    IEnumerator ActivateCheckPoint()
    {
        renderer.material = materials[1];
        rotationSpeed = 1000f;
        transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

        yield return new WaitForSeconds(checkpointActivationSpeed);

        rotationSpeed = 25;
    }
}
