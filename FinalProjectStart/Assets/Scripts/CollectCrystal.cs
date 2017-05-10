using UnityEngine;
using System.Collections;


public class CollectCrystal : MonoBehaviour {

    public GameObject CrystalGlow;

	[SerializeField]
	private GameObject playerTurret1;
	[SerializeField]
	private GameObject playerTurret2;

	private bool isCollected;

	// Use this for initialization
	void Start () {
	
		CrystalGlow.SetActive(true);
		isCollected = false;

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider obj)
    {
		if(obj.tag == "Character1" && isCollected == false)
        {
			Fire fire1 = playerTurret1.GetComponent<Fire> () as Fire;
            CrystalGlow.SetActive(false);

			fire1.Ammo += 3;
			isCollected = true;
        }

		if(obj.tag == "Character2" && isCollected == false)
		{
			Fire fire2 = playerTurret2.GetComponent<Fire> () as Fire;
			CrystalGlow.SetActive(false);

			fire2.Ammo += 3;
			isCollected = true;
		}
    }
}
