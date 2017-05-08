using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Characters.FirstPerson;

public class CameraController : MonoBehaviour
{
    public CanvasGroup myCG;
    [SerializeField] GameObject frontCamera;
    [SerializeField] GameObject selfieCamera;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] List<Material> materials;
    [SerializeField] GameObject currentCamera;

    private RawImage gameOverImage;

    private Player player;
    private bool flash = false;
    private NPC npc;
    private Renderer rend;

    void Start()
    {
        gameOverPanel.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        npc = gameObject.GetComponentInParent<NPC>();
        StartCoroutine(WaitForPicture());
    }

    private IEnumerator WaitForPicture()
    {
        Debug.Log("Waiting for picture");
        yield return new WaitForSeconds(Random.Range(2, 5));
        StartCoroutine(StartCountdown(currentCamera));
    }

    void FixedUpdate()
    {
        if (flash)
        {
            FlashCamera();
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            StartCoroutine(StartCountdown(frontCamera));
        }

    }

    private void Update()
    {
        Ray debugRay = new Ray(currentCamera.transform.position, currentCamera.gameObject.transform.forward);
        Debug.DrawRay(debugRay.origin, debugRay.direction);
    }

    private IEnumerator StartCountdown(GameObject cameraToUse)
    {
        Debug.Log("Ready to take picture");
        rend = cameraToUse.GetComponentInChildren<Renderer>();
        rend.enabled = true;

        for (int i = 0; i < materials.Count; i++)
        {
            rend.sharedMaterial = materials[i];
            yield return new WaitForSeconds(.3f);
        }

        TakePicture(cameraToUse);
    }

    private void TakePicture(GameObject cameraToUse)
    {
        Debug.Log("Taking picture");
        cameraToUse.GetComponentInChildren<ParticleSystem>().Play();

        Ray ray = new Ray(cameraToUse.transform.position, cameraToUse.gameObject.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Debug.DrawRay(ray.origin, ray.direction);
            if (hit.collider.tag.Equals("Player"))
            {
                StopAllCoroutines();
                GameOver();
            }

        }

        StartCoroutine(WaitForPicture());
        rend.sharedMaterial = materials[0];
    }

    private void GameOver()
    {
        gameObject.GetComponent<AudioSource>().Play();

        Debug.Log("You got hit, dawg");
        flash = true;
        myCG.alpha = 1;

        gameOverPanel.SetActive(true);
        gameOverImage = GameObject.Find("GameOverImage").GetComponent<RawImage>();
        gameOverImage.texture = gameObject.GetComponentInChildren<Camera>().targetTexture;
        player.gameObject.GetComponent<FirstPersonController>().enabled = false;
    }

    private void FlashCamera()
    {
        Debug.Log("This works");
        myCG.alpha = myCG.alpha - Time.deltaTime * 0.3f;
        if (myCG.alpha <= 0)
        {
            myCG.alpha = 0;
            flash = false;
            Destroy(myCG.gameObject);
            Destroy(this);
        }
    }
}
