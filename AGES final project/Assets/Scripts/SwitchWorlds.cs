using UnityEngine;
using System.Collections;

public class SwitchWorlds : MonoBehaviour
{
    [SerializeField]
    Transform world2Position;
    [SerializeField]
    Transform world1Position;

    Player player;
    Animator animator;
    bool isInWorld2;
	// Use this for initialization
	void Start ()
    {
        player = gameObject.GetComponentInParent<Player>();
        animator = gameObject.GetComponentInChildren<Animator>();
        isInWorld2 = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (!isInWorld2)
        {
            if (Input.GetButtonDown("Warp") && player.isAlive)
            {
                animator.SetBool("HasWarped", true);
                this.transform.position = world2Position.position;
                isInWorld2 = true;
                StartCoroutine(ChangeAnimation());
            }
        }
        else
        {
            if (Input.GetButtonDown("Warp") && player.isAlive)
            {
                animator.SetBool("HasWarped", true);
                this.transform.position = world1Position.position;
                isInWorld2 = false;
                StartCoroutine(ChangeAnimation());
            }
        }
    }

    private IEnumerator ChangeAnimation()
    {
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("HasWarped", false);
    }
}
