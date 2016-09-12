using UnityEngine;
using System.Collections;

public class HealthTracker : MonoBehaviour
{

    public int health = 100;
    public int maxHealth = 100;
    public bool damagedThisFrame = false;

    // Use this for initialization
    void Start()
    {
        health = maxHealth;
    }

    void Update()
    {
        if (health < 0)
        {
            Destroy(gameObject);
        }
        damagedThisFrame = false;
    }

    // Getting hit by a damager causes damage
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Damager")
        {
            health -= collision.collider.GetComponent<damager>().damageDealt;
            damagedThisFrame = true;
        }
    }
}
