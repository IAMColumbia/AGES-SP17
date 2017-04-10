using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour
{
    [SerializeField]
    private string shootButton;
    [SerializeField]
    private string rightStickVertical;
    [SerializeField]
    private string rightStickHorizontal;
    [SerializeField]
    private Transform reticle;
    [SerializeField]
    private Rigidbody bullet;
    [SerializeField]
    private float bulletSpeed;

    [SerializeField]
    float maxDistanceToActivate = 10;
    [SerializeField]
    LayerMask layerToCheckForEnemies;

    private const float zeroConstant = 0;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        Shoot();
	}

    void Shoot()
    {
        if (Input.GetButton(shootButton))
        {
            Vector3 endpoint = (transform.forward * maxDistanceToActivate) + transform.position;

            RaycastHit raycastHit;

            EnemyAI enemy;

            Debug.DrawLine(transform.position, endpoint, Color.green, 2);

            if (Physics.Raycast(transform.position,transform.forward, out raycastHit,maxDistanceToActivate,layerToCheckForEnemies))
            {
                enemy = raycastHit.transform.gameObject.GetComponent<EnemyAI>();

                if (enemy != null)
                {
                    enemy.Explode();
                }
            }
        }
    }

    //void RotateToReticle()
    //{
    //    gameObject.transform.LookAt(reticle);
    //}
}
