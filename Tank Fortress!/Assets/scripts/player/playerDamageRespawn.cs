using UnityEngine;
using System.Collections;

public class playerDamageRespawn : MonoBehaviour {

    private AudioSource source;
    public AudioClip death;
    public AudioClip respawn;

    public Transform respawner;
    public bool spawnerDied = false;

    public float damageMultiplier = 1.0f;
    public int health = 9;
    public int maxHealth = 9;

    public int respawnWait = 80;
    public int respawnWaitRemaining = -1;

    Vector3 normalScale;

    void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    // Use this for initialization
    void Start()
    {
        health = maxHealth;
        normalScale = transform.localScale;
    }

    void Update()
    {
        if (health <= 0 && respawnWaitRemaining < 0)
        {
            initRespawn();
        }
        else if (health <= 0 && respawnWaitRemaining == 0)
        {
            finishRespawn();
        }
    }

    void FixedUpdate()
    {
        if (health <= 0 && respawnWaitRemaining > 0)
        {
            respawnWaitRemaining--;
            transform.position = respawner.transform.position + Vector3.up * 2.0f;
        }
    }

    // Getting hit by a damager causes damage
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Damager" && health >= 0)
        {
            health -= (int)(damageMultiplier * (float)collision.collider.GetComponent<damager>().damageDealt);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Damager" && health >= 0)
        {
            health -= (int)(damageMultiplier * (float)collider.GetComponent<damager>().damageDealt);
        }
    }

    Vector3 respawningScale = new Vector3(0.01f, 0.01f, 0.01f);
    void initRespawn()
    {
        source.PlayOneShot(death);
        if (spawnerDied) Destroy(gameObject);
        else
        {
            transform.position = respawner.transform.position + Vector3.up * 2.0f;
            transform.localScale = respawningScale;
            respawnWaitRemaining = respawnWait;
        }
    }

    void finishRespawn()
    {
        source.PlayOneShot(respawn);
        health = maxHealth;
        transform.localScale = normalScale;
        respawnWaitRemaining = -1;
    }
}
