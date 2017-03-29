using UnityEngine;
using System.Collections;

//NOTE:  I haven't incorporated this into the game, but I have the scripts ready in case I do want to use it later

public class DeadTank : MonoBehaviour
{
    [SerializeField] float layMineCooldown;
    [SerializeField] GameObject minePrefab;

    [HideInInspector] public int PlayerNumber;

    string FireButton;
    bool canLayMine = true;

	// Use this for initialization
	void Start ()
    {
        FireButton = "Fire1_P" + PlayerNumber;
    }
	
	// Update is called once per frame
	void Update ()
    {
        LayMine();
    }

    void LayMine()
    {
        if (Input.GetButtonDown(FireButton) && canLayMine)
        {
            StartCoroutine(MineCooldown());
        }
    }

    IEnumerator MineCooldown()
    {
        canLayMine = false;
        Vector3 spawnLoaction = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z);
        Instantiate(minePrefab, spawnLoaction, Quaternion.identity);
        yield return new WaitForSeconds(layMineCooldown);
        canLayMine = true;
    }
}
