using UnityEngine;
using System.Collections;

public class landmine : MonoBehaviour {
    public int lifeSpan = 1;
    public Transform explosionPrefab;

    void Update()
    {
        if (lifeSpan < 0)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag != "Platform")
        {
            Transform explosionClone = Instantiate(explosionPrefab);
            explosionClone.transform.position = transform.position;
            lifeSpan = -1; // destroy this on the next cycle
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Damager")
        {
            Transform explosionClone = Instantiate(explosionPrefab);
            explosionClone.transform.position = transform.position;
            lifeSpan = -1; // destroy this on the next cycle
        }
    }
}
