using UnityEngine;
using System.Collections;

// Borrowing heavily from
// https://unity3d.com/learn/tutorials/topics/2d-game-creation/creating-basic-platformer-game

public class playerControl : MonoBehaviour {

    public bool m_facingRight = true;
    public bool m_jump = false;
    public float m_moveForce = 200f;
    public float m_maxSpeed = 8f;
    public float m_jumpForce = 500f;
    public Transform m_groundCheck;

    public bool m_grounded = false;
    private Rigidbody2D rb2d;
    public Vector2 m_up = Vector2.up;

	// Use this for initialization
	void Awake () {
        rb2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            m_up = transform.position - m_groundCheck.position;
            m_up.Normalize();
            m_grounded = Physics2D.Linecast(transform.position, m_groundCheck.position, 1 << LayerMask.NameToLayer("Platforms"));
        }

        if (Input.GetButtonDown("Jump") && m_grounded)
        {
            m_jump = true;
            m_grounded = false;
        }

        // Keep the player from tipping over
        Quaternion rotation = rb2d.transform.rotation;
        if (rotation.z > 0.38f) {
            rb2d.transform.rotation = new Quaternion(rotation.x, rotation.y, 0.38f, rotation.w);
        }
        else if (rotation.z < -0.38f)
        {
            rb2d.transform.rotation = new Quaternion(rotation.x, rotation.y, -0.38f, rotation.w);
        }
	}

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        if (h * rb2d.velocity.x < m_maxSpeed)
        {
            rb2d.AddForce(Vector2.right * h * m_moveForce);
        }
        // clamp horizontal speed
        if (Mathf.Abs(rb2d.velocity.x) > m_maxSpeed)
        {
            rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * m_maxSpeed, rb2d.velocity.y);
        }


        if (h > 0 && !m_facingRight)
            Flip();
        else if (h < 0 && m_facingRight)
            Flip();

        if (m_jump)
        {
            rb2d.AddForce(m_up * m_jumpForce);
            m_jump = false;
        }
    }

    void Flip()
    {
        m_facingRight = !m_facingRight;
        Vector3 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
    }
}
