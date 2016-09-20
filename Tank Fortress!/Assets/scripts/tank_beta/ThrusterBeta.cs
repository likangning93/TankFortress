using UnityEngine;
using System.Collections;

public class ThrusterBeta : MonoBehaviour {

    private AudioSource source;
    public AudioClip fireRocket;

    public Transform flameHandle;
    public GameObject tankHandle;
    public ThrusterButton button;

    Rigidbody2D tankRb2d;

    public int firingDuration = 120;
    public int rechargeDuration = 240;
    public int m_currentFiring = -1;
    public int m_currentRecharge = -1;
    Vector3 originalFlameScale;

    void Awake()
    {
        source = GetComponent<AudioSource>();
    }

	// Use this for initialization
	void Start () {
        tankRb2d = tankHandle.GetComponent<Rigidbody2D>();
        m_currentFiring = -1;
        m_currentRecharge = rechargeDuration;
        originalFlameScale = flameHandle.transform.localScale;
        flameHandle.transform.localScale = originalFlameScale * (0.0f);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (m_currentFiring > 0)
        {
            tankRb2d.AddForceAtPosition(Vector3.up * m_currentFiring * 5.0f, transform.position);
            flameHandle.transform.localScale = originalFlameScale * ((float) m_currentFiring / (float) firingDuration);
            flameHandle.Rotate(0.0f, 10.0f, 0.0f);
            m_currentFiring--;
        }
        else if (m_currentRecharge < rechargeDuration)
        {
            m_currentRecharge++;
            if (m_currentRecharge == rechargeDuration - 1)
            {
                button.buttonUp();
            }
        }
	}

    public void fire()
    {
        if (m_currentRecharge == rechargeDuration)
        {
            source.PlayOneShot(fireRocket);
            m_currentFiring = firingDuration;
            m_currentRecharge = 0;
        }
    }

    public void cancel()
    {
        if (m_currentFiring > 0) m_currentFiring = 1;
    }
}
