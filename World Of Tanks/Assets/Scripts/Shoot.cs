using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {

    public GameObject shellPrefab;
    public Transform shellSpawn;

    [SerializeField] string shootInput;

    private float speed = 20;
    private float bulletTimer = 0;

    void Start ()
    {
        shellSpawn = GetComponentInChildren<Test>().GetComponent<Transform>();
	}
	
	void Update ()
    {
        bulletTimer -= Time.deltaTime;

        if (Input.GetAxis(shootInput) != 0 && bulletTimer <= 0)
        {
            Fire();
        }
    }

    void Fire()
    {
        bulletTimer = 2;

        var bullet = (GameObject)Instantiate(shellPrefab, shellSpawn.position, shellSpawn.rotation);

        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * speed;

        Destroy(bullet, 2.0f);
    }

}
