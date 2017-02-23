﻿using UnityEngine;
using System.Collections;

public class Strafe : MonoBehaviour {
    [SerializeField]
    float speed, timeOffset;

    [SerializeField]
    AnimationCurve position;

    float t;
	// Use this for initialization
	void Start () {
        t = Time.time - timeOffset;
	}
	
	// Update is called once per frame
	void Update () {
        t += speed * Time.deltaTime;

        Vector3 pos = transform.position;

        pos.x = position.Evaluate(t);

        transform.position = pos;
	}
}
