using UnityEngine;
using System.Collections;

public class RollerDamageTracker : MonoBehaviour {

    public HealthTracker parentHealthTracker;
    public float damageMultiplier = 1.0f;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Roller" &&
            !parentHealthTracker.damagedThisFrame)
        {
            parentHealthTracker.damagedThisFrame = true;
            parentHealthTracker.health -= (int)(damageMultiplier * (float)collision.collider.GetComponent<damager>().damageDealt);
            //print("damage from sub tracker roller collision");
        }
    }
}
