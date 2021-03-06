﻿using UnityEngine;
using System.Collections;

public class cannonball : MonoBehaviour {

    public int lifeRemaining = 10000;

    public Transform explosionPrefab;

	// Use this for initialization
	void FixedUpdate () {
        lifeRemaining--;
	}

    void Update()
    {
        if (lifeRemaining < 0)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D()
    {
        Transform explosionClone = Instantiate(explosionPrefab);
        explosionClone.transform.position = transform.position;
        lifeRemaining = -1; // destroy this on the next cycle
    }
}
