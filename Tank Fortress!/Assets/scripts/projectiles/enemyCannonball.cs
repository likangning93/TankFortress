using UnityEngine;
using System.Collections;

public class enemyCannonball : MonoBehaviour {

    public int lifeRemaining = 10000;
    public Transform explosionPrefab;

    // Use this for initialization
    void FixedUpdate()
    {
        lifeRemaining--;
    }

    void Update()
    {
        if (lifeRemaining < 0)
        {
            Transform explosionClone = Instantiate(explosionPrefab);
            explosionClone.transform.position = transform.position;
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
        {
            lifeRemaining = -1; // destroy this on the next cycle
        }
    }
}
