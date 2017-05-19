using UnityEngine;
using System.Collections;

public class WaterZoneScript : MonoBehaviour {

    // Use this for initialization
    Animator animator;

    [SerializeField]
    GameObject goalSphereToggle;

    public bool HasGoal
    {
        get
        {
            return goalSphereToggle.activeSelf;
        }
    }
    void Start () {
        animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        checkWaterZone();
	}
    private void checkWaterZone()
    {
        if (HasGoal == false)
        {          
            animator.SetBool("hasGoal", false);
        }
        else if (HasGoal == true)
        {
            animator.SetBool("hasGoal", true);
        }
       }
}
