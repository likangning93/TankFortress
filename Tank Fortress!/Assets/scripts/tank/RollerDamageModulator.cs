﻿using UnityEngine;
using System.Collections;

public class RollerDamageModulator : MonoBehaviour {

    public damager rollerDamager;
    Rigidbody2D rb2d;

	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        rollerDamager.damageDealt = Mathf.Abs(rb2d.angularVelocity) > 100.0f ? 10 : 0;
	}
}
