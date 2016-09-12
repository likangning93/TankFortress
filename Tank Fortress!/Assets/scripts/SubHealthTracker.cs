using UnityEngine;
using System.Collections;

public class SubHealthTracker : MonoBehaviour {

    public HealthTracker parentHealthTracker;
    public int damageMultiplier = 1;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Damager" &&
            !parentHealthTracker.damagedThisFrame)
        {
            parentHealthTracker.damagedThisFrame = true;
            parentHealthTracker.health -= damageMultiplier * collision.collider.GetComponent<damager>().damageDealt;
            print("damage from sub tracker collision");
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Damager" &&
            !parentHealthTracker.damagedThisFrame)
        {
            parentHealthTracker.damagedThisFrame = true;
            parentHealthTracker.health -= damageMultiplier * collider.GetComponent<damager>().damageDealt;
            print("damage from sub tracker trigger");
        }
    }
}
