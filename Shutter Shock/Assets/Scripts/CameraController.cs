using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class CameraController : MonoBehaviour
{
    public CanvasGroup myCG;
    [SerializeField] GameObject frontCamera;
    [SerializeField] GameObject selfieCamera;
    [SerializeField] List<Material> materials;

    private Player player;
    private bool flash = false;
    private NPC npc;
    private Renderer rend;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        npc = gameObject.GetComponentInParent<NPC>();
    }

    void FixedUpdate()
    {
        if (flash)
        {
            Debug.Log("This works");
            myCG.alpha = myCG.alpha - Time.deltaTime * 0.3f;
            if (myCG.alpha <= 0)
            {
                myCG.alpha = 0;
                flash = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            StartCoroutine(StartCountdown(frontCamera));
        }

    }

    private void Update()
    {
        Ray debugRay = new Ray(frontCamera.transform.position, frontCamera.gameObject.transform.forward);
        Debug.DrawRay(debugRay.origin, debugRay.direction);
    }

    private IEnumerator StartCountdown(GameObject cameraToUse)
    {
        rend = cameraToUse.GetComponentInChildren<Renderer>();
        rend.enabled = true;

        for (int i = 0; i < materials.Count; i++)
        {
            rend.sharedMaterial = materials[i];
            yield return new WaitForSeconds(.5f);
        }

        TakePicture(cameraToUse);
    }

    private void TakePicture(GameObject cameraToUse)
    {
        cameraToUse.GetComponentInChildren<ParticleSystem>().Play();

        Ray ray = new Ray(cameraToUse.transform.position, cameraToUse.gameObject.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log("Sending raycast");
            Debug.DrawRay(ray.origin, ray.direction);
            if (hit.collider.tag.Equals("Player"))
            {
                Debug.Log("You got hit, dawg");
            }

        }

        if (player.IsPlayerScrewed())
        {
            flash = true;
            myCG.alpha = 1;
        }

        else
        {

        }
    }
}
