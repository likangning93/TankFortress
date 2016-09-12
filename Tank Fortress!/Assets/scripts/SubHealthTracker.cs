using UnityEngine;
using System.Collections;

public class SubHealthTracker : MonoBehaviour {

    public HealthTracker parentHealthTracker;
    public float damageMultiplier = 1.0f;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Damager" &&
            !parentHealthTracker.damagedThisFrame)
        {
            parentHealthTracker.damagedThisFrame = true;
            parentHealthTracker.health -= (int) (damageMultiplier * (float) collision.collider.GetComponent<damager>().damageDealt);
            //print("damage from sub tracker collision");
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Damager" &&
            !parentHealthTracker.damagedThisFrame)
        {
            parentHealthTracker.damagedThisFrame = true;
            parentHealthTracker.health -= (int)(damageMultiplier * (float) collider.GetComponent<damager>().damageDealt);
            //print("damage from sub tracker trigger");
        }
    }
}
