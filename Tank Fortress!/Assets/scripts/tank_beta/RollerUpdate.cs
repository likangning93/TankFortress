using UnityEngine;
using System.Collections;

public class RollerUpdate : MonoBehaviour {

    public float m_maxAngularVelocity = 600.0f;
    public DriveControlBeta controller;
    Rigidbody2D rb2d;

	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        DriveState tankState = controller.tankState;
        if (tankState != DriveState.NEUTRAL)
        {
            float torque = controller.m_torqueMagnitude;
            if (tankState == DriveState.FORWARD)
            {
                torque = -torque;
            }

            rb2d.AddTorque(torque);
            if (rb2d.angularVelocity < -m_maxAngularVelocity) rb2d.angularVelocity = -m_maxAngularVelocity;
            if (rb2d.angularVelocity > m_maxAngularVelocity) rb2d.angularVelocity = m_maxAngularVelocity;
        }
	}
}
