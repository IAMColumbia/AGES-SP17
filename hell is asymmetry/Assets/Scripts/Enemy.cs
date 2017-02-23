using UnityEngine;
using System.Collections;

public enum Phase
{
    A,
    B
}

public class Enemy : MonoBehaviour {

    public bool Alive { get; private set; }

    HalfEnemy[] Halves;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
