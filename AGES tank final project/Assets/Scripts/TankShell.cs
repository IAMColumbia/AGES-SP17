using UnityEngine;
using System.Collections;

public class TankShell : MonoBehaviour
{
    [SerializeField]
    private float maxLifetime = 2f;
    [SerializeField]
    private float stunDuration = 1f;
    private TankMovement tankMovementPlayer1;
    private TankMovement tankMovementPlayer2;
    private TankMovement tankMovementPlayer3;
    private TankMovement tankMovementPlayer4;
    private MeshRenderer shellmMeshRenderer;
    private Collider shellcollider;
    
    

	// Use this for initialization
	void Start ()
    {
        Destroy(this.gameObject, maxLifetime);
        tankMovementPlayer1 = GameObject.FindGameObjectWithTag("Player").GetComponent<TankMovement>();
        tankMovementPlayer2 = GameObject.FindGameObjectWithTag("Player2").GetComponent<TankMovement>();
        tankMovementPlayer3 = GameObject.FindGameObjectWithTag("Player3").GetComponent<TankMovement>();
        tankMovementPlayer4 = GameObject.FindGameObjectWithTag("Player4").GetComponent<TankMovement>();
        shellmMeshRenderer = gameObject.GetComponent<MeshRenderer>();
        shellcollider = gameObject.GetComponent<Collider>();
	}

    void OnTriggerEnter(Collider other)
    {
        
        if(other == GameObject.FindGameObjectWithTag("Player").GetComponent<Collider>())
        {
            StartCoroutine(HandleStunForPlayer1());
        }
        else if(other == GameObject.FindGameObjectWithTag("Player2").GetComponent<Collider>())
        {
            StartCoroutine(HandleStunForPlayer2());
        }
        else if (other == GameObject.FindGameObjectWithTag("Player3").GetComponent<Collider>())
        {
            StartCoroutine(HandleStunForPlayer3());
        }
        else if (other == GameObject.FindGameObjectWithTag("Player4").GetComponent<Collider>())
        {
            StartCoroutine(HandleStunForPlayer4());
        }

    }

    private IEnumerator HandleStunForPlayer1()
    {
        tankMovementPlayer1.enabled = false;
        shellmMeshRenderer.enabled = false;
        shellcollider.enabled = false;
        yield return new WaitForSeconds(stunDuration);
        tankMovementPlayer1.enabled = true;
    }

    private IEnumerator HandleStunForPlayer2()
    {
        tankMovementPlayer2.enabled = false;
        shellmMeshRenderer.enabled = false;
        shellcollider.enabled = false;
        yield return new WaitForSeconds(stunDuration);
        tankMovementPlayer2.enabled = true;
    }

    private IEnumerator HandleStunForPlayer3()
    {
        tankMovementPlayer3.enabled = false;
        shellmMeshRenderer.enabled = false;
        shellcollider.enabled = false;
        yield return new WaitForSeconds(stunDuration);
        tankMovementPlayer3.enabled = true;
    }

    private IEnumerator HandleStunForPlayer4()
    {
        tankMovementPlayer4.enabled = false;
        shellmMeshRenderer.enabled = false;
        shellcollider.enabled = false;
        yield return new WaitForSeconds(stunDuration);
        tankMovementPlayer4.enabled = true;
    }
}

