using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpdateCrystalText : MonoBehaviour {

	[SerializeField]
	private GameObject playerAmmo;

	private Text text;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		CheckAmmoAmount ();
	
	}

	private void CheckAmmoAmount()
	{
		text = gameObject.GetComponent<Text> () as Text;
		text.text = playerAmmo.GetComponent<Fire> ().Ammo.ToString ();
	}
}
