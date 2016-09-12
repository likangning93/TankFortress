using UnityEngine;
using System.Collections;

public class landmine : MonoBehaviour {
    public int lifeSpan = 1;

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
            lifeSpan = -1; // destroy this on the next cycle
        }
    }
}
