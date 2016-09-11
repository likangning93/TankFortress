using UnityEngine;
using System.Collections;

public class cannonball : MonoBehaviour {

    public int lifeSpan = 10000;

	// Use this for initialization
	void FixedUpdate () {
        lifeSpan--;
	}

    void Update()
    {
        if (lifeSpan < 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        lifeSpan = -1; // destroy this on the next cycle
    }
}
