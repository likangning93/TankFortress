using UnityEngine;
using System.Collections;

// Borrowing heavily from
// https://unity3d.com/learn/tutorials/topics/2d-game-creation/creating-basic-platformer-game

public class playerControlBeta : MonoBehaviour
{

    public bool m_jump = false;
    public float m_moveForce = 3f;
    public float m_maxSpeed = 15f;
    public float m_maxDropSpeed = 30f;
    public float m_jumpForce = 9f;
    public Transform m_groundCheck;

    public GameObject tank;

    public int m_grounded = 0;
    private Rigidbody2D rb2d;
    public Vector2 m_up = Vector2.up;
    private Rigidbody2D tankRb2d;

    public float m_moveX;

    // Use this for initialization
    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        tankRb2d = tank.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        // check grounding state
        m_up = transform.position - m_groundCheck.position;
        m_up.Normalize();
        m_grounded = Physics2D.Linecast(transform.position, m_groundCheck.position, 1 << LayerMask.NameToLayer("Platforms")) ? 2 : m_grounded;

        if (Input.GetButtonDown("Jump") && m_grounded > 0)
        {
            m_jump = true;
            m_grounded--;
        }

        // Keep the player from tipping over
        Quaternion rotation = rb2d.transform.rotation;
        if (rotation.z > 0.30f)
        {
            rb2d.transform.rotation = new Quaternion(rotation.x, rotation.y, 0.30f, rotation.w);
        }
        else if (rotation.z < -0.30f)
        {
            rb2d.transform.rotation = new Quaternion(rotation.x, rotation.y, -0.30f, rotation.w);
        }
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        rb2d.AddForce(Vector2.right * h * m_moveForce);
        // clamp horizontal speed relative to the tank's speed
        if (Mathf.Abs(rb2d.velocity.x - tankRb2d.velocity.x) > m_maxSpeed)
        {
            float newVelocityX = Mathf.Sign(rb2d.velocity.x) * m_maxSpeed + tankRb2d.velocity.x;
            rb2d.velocity = new Vector2(newVelocityX, rb2d.velocity.y);
        }
        m_moveX = rb2d.velocity.x;

        float v = Input.GetAxis("Vertical");
        if (v < 0.0f)
        {
            rb2d.AddForce(Vector2.up * v * m_moveForce);
        }
        // clamp drop speed
        if (Mathf.Abs(rb2d.velocity.y) > m_maxDropSpeed && rb2d.velocity.y < 0.0f)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, -m_maxSpeed);
        }

        if (m_jump)
        {
            rb2d.AddForce(m_up * m_jumpForce);
            m_jump = false;
        }
    }
}
