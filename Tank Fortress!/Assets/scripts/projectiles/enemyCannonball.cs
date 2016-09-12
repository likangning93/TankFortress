using UnityEngine;
using System.Collections;

public class enemyCannonball : MonoBehaviour {

    public int lifeRemaining = 10000;

    // Use this for initialization
    void FixedUpdate()
    {
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
        lifeRemaining = -1; // destroy this on the next cycle
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Damager")
            lifeRemaining = -1; // destroy this on the next cycle
    }
}
